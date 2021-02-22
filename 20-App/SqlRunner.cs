using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Baz.Core;

namespace myCoreMvc.App
{
    public static class SqlRunner
    {
        public static void Run(string relativeScriptPath)
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
