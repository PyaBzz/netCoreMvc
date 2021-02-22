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
            var argCount = args.Count();
            var lastArg = argCount > 0 ? args[argCount - 1] : string.Empty; //Todo: Add an extension
            if (lastArg == "init" || lastArg == "destroy")
            {
                var config = ConfigFactory.Get();
                string scriptRelPath;
                if (lastArg == "init")
                    scriptRelPath = config.Database.ScriptPath["Init"];
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
    }
}
