﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Baz.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using myCoreMvc.App.Providing;

namespace myCoreMvc.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var lastArg = args[args.Count() - 1]; //Todo: Add an extension
            if (lastArg == "init" || lastArg == "destroy")
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("settings.json")
                    .Build();
                var connectionStr = config.GetValue<string>("ConnectionString");
                string scriptRelPath;
                if (lastArg == "init")
                    scriptRelPath = config.GetValue<string>("initScriptRelPath");
                else
                    scriptRelPath = config.GetValue<string>("destroyScriptRelPath");
                SqlRunner.Run(connectionStr, scriptRelPath);
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
