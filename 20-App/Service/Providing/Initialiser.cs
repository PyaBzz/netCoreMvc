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
            var directory = Assembly.GetExecutingAssembly().GetDirectory();
            var longPath = Path.Combine(directory, "..//..//..//..//20-App//Sql//Initial.sql");
            var relativePath = Path.GetFullPath(longPath);
            Console.WriteLine(relativePath);
            var queryStr = File.ReadAllText(relativePath);
            Console.WriteLine(queryStr);

            Console.WriteLine("Connecting to SQL server");
            using (var connection = new SqlConnection(connectionStr))
            {
                connection.Open();
                var command = new SqlCommand(queryStr, connection);
                var result = command.ExecuteNonQuery();
                Console.WriteLine(result);
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
