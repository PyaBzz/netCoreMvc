using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Baz.Core;

namespace myCoreMvc.App.Providing
{
    public static class Initialiser
    {
        public static void Run(string connectionStr, string dbName)
        {
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory();
            var scriptPath = Path.Combine(outputDir, "Sql//Init.sql");
            var scriptText = File.ReadAllText(scriptPath);

            Console.WriteLine("Connecting to SQL server");
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var command = new SqlCommand(scriptText, connection);
                var result = command.ExecuteNonQuery();
                if (result == -1)
                    Console.WriteLine("Database has been created successfully!");
            }
        }

        private static string ScriptFolder
        {
            get
            {
                return typeof(Initialiser).FullName;
            }
        }
    }
}
