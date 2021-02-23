using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Baz.Core;

namespace myCoreMvc.App
{
    public static class DbMigrator
    {
        public static bool MigrateIfNeeded(string[] args)
        {
            var lastArg = GetLast(args).ToLower();
            if (ShouldMigrate(lastArg))
            {
                var config = ConfigFactory.Get();
                string scriptRelPath;
                if (lastArg == "make")
                    scriptRelPath = config.Database.ScriptPath["Make"];
                else if (lastArg == "populate")
                    scriptRelPath = config.Database.ScriptPath["Populate"];
                else
                    scriptRelPath = config.Database.ScriptPath["Destroy"];
                Run(scriptRelPath);
                Console.WriteLine("Migration done");
                return true;
            }
            return false;
        }

        private static string GetLast(string[] args)
        {
            var paramCount = args.Count();
            return paramCount > 0 ? args[paramCount - 1] : string.Empty; //Todo: Add an extension
        }

        private static bool ShouldMigrate(string lastArg)
        {
            return lastArg == "make" || lastArg == "populate" || lastArg == "destroy";
        }

        private static void Run(string relativeScriptPath)
        {
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory(); //Todo: If merged into the extension method what assembly dir does it return?
            var scriptPath = Path.Combine(outputDir, relativeScriptPath);
            var scriptText = File.ReadAllText(scriptPath);
            var scriptBatches = Regex.Split(scriptText, "go", RegexOptions.IgnoreCase);
            var batchCount = scriptBatches.Count();

            Console.WriteLine("Connecting to SQL server");
            using (var connection = SqlConFactory.Get(true))
            {
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                Console.WriteLine($"Running {batchCount} sql batches");
                for (var i = 0; i < batchCount; i++)
                {
                    command.CommandText = scriptBatches[i];
                    var result = command.ExecuteNonQuery();
                    if (result == -1)
                        Console.WriteLine($"Batch {i} executed");
                }
            }
        }
    }
}
