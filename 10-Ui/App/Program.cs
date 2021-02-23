using System;
using System.IO;
using System.Linq;
using Baz.Core;
using Microsoft.AspNetCore.Hosting;
using myCoreMvc.App;

namespace myCoreMvc.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lastArg = GetLast(args).ToLower();
            if (IsDbMigration(lastArg))
            {
                var config = ConfigFactory.Get();
                string scriptRelPath;
                if (lastArg == "make")
                    scriptRelPath = config.Database.ScriptPath["Make"];
                else if (lastArg == "populate")
                    scriptRelPath = config.Database.ScriptPath["Populate"];
                else
                    scriptRelPath = config.Database.ScriptPath["Destroy"];
                SqlRunner.Run(scriptRelPath);
            }
            else
            {
                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    //.UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();
                host.Run();
            }
        }

        private static bool IsDbMigration(string lastArg)
        {
            return lastArg == "make" || lastArg == "populate" || lastArg == "destroy";
        }

        private static string GetLast(string[] args)
        {
            var paramCount = args.Count();
            return paramCount > 0 ? args[paramCount - 1] : string.Empty; //Todo: Add an extension
        }
    }
}
