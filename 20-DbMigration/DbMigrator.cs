using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App;
using myCoreMvc.App.Services;

namespace myCoreMvc.DbMigrations
{
    public static class DbMigrator
    {
        private static readonly Dictionary<string, string> dbs = new Dictionary<string, string> { { "test", "bazDbTest" }, { "prod", "bazDb" } };
        private static readonly Dictionary<string, string> ops = new Dictionary<string, string> { { "Destroy", "Destroy" }, { "Make", "Make" }, { "Populate", "Populate" } };
        private static readonly string dbNamePlaceholder = "##dbname##";
        private static readonly IDbConFactory dbConFactory = new DbConFactory();
        public static void Go(string db, string op)
        {
            string dbname, operation;
            if (dbs.TryGetValue(db, out dbname) == false)
                throw new ArgumentException($"Invalid database specified {db}. Database must be in [{dbs.Keys.ToString(", ")}]");

            if (ops.TryGetValue(op, out operation) == false)
                throw new ArgumentException($"Invalid operation specified {op}. Operation must be in [{ops.Keys.ToString(", ")}]");

            var scrRelPath = ConfigFactory.Get().Data.Path.Script[operation];
            RunScript(scrRelPath, dbname);
            Console.WriteLine("Migration done");
            Console.WriteLine();
        }

        private static void RunScript(string relPath, string dbname)
        {
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory(); //Todo: If merged into the extension method what assembly dir does it return?
            var absPath = Path.Combine(outputDir, relPath);
            var scrText = File.ReadAllText(absPath).Replace(dbNamePlaceholder, dbname);
            var scrBatches = Regex.Split(scrText, "go", RegexOptions.IgnoreCase);
            var batchCount = scrBatches.Count();

            Console.WriteLine("Connecting to SQL server");
            Console.WriteLine($"Executing script: {absPath}");
            Console.WriteLine($"On database: {dbname}");
            using (var conn = dbConFactory.GetInit())
            {
                conn.Open();
                var command = new SqlCommand();
                command.Connection = conn;
                Console.WriteLine($"Running {batchCount} sql batches");
                for (var i = 0; i < batchCount; i++)
                {
                    command.CommandText = scrBatches[i];
                    var result = command.ExecuteNonQuery();
                    if (result == -1)
                        Console.WriteLine($"Batch {i} executed");
                }
            }
        }
    }
}
