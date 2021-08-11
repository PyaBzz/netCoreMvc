using System;
using Microsoft.Extensions.DependencyInjection;

namespace myCoreMvc.DbMigrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (DbMigrator.MigrateIfNeeded(args))
            {
                return;
            }
            else
            {
                Console.WriteLine("No migration specified");
            }
        }
    }
}
