using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App;

namespace myCoreMvc.DbMigrations
{
    public static class DbMigrator
    {
        private static readonly Dictionary<string, string> dbs = new Dictionary<string, string> { { "test", "bazDbTest" }, { "prod", "bazDb" } };
        private static readonly Dictionary<string, string> ops = new Dictionary<string, string> { { "Destroy", "Destroy" }, { "Make", "Make" }, { "Populate", "Populate" } };
        private static readonly string dbNamePlaceholder = "##dbname##";
        public static void Go(string db, string op)
        {
            string dbname, operation;
            if (dbs.Keys.Contains(db))
                dbname = dbs[db];
            else
                throw new ArgumentException($"Invalid database specified {db}. Database must be among {dbs.Keys.ToString(", ")}.");

            if (ops.Keys.Contains(op))
                operation = ops[op];
            else
                throw new ArgumentException($"Invalid operation specified {op}. Operation must be among {ops.Keys.ToString(", ")}.");

            var scriptRelPaths = ConfigFactory.Get().Data.Path.Script;
            var theRelPath = scriptRelPaths[operation];
            Run(dbname, theRelPath);
            Console.WriteLine("Migration done");
            Console.WriteLine();
        }

        private static void Run(string dbname, string relativeScriptPath)
        {
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory(); //Todo: If merged into the extension method what assembly dir does it return?
            var scriptPath = Path.Combine(outputDir, relativeScriptPath);
            var scriptText = File.ReadAllText(scriptPath).Replace(dbNamePlaceholder, dbname);
            var scriptBatches = Regex.Split(scriptText, "go", RegexOptions.IgnoreCase);
            var batchCount = scriptBatches.Count();

            Console.WriteLine("Connecting to SQL server");
            Console.WriteLine($"Executing script: {scriptPath}");
            Console.WriteLine($"On database: {dbname}");
            using (var conn = SqlConFactory.Get(true))
            {
                conn.Open();
                var command = new SqlCommand();
                command.Connection = conn;
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
