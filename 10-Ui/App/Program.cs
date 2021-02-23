using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using myCoreMvc.App;

namespace myCoreMvc.UI
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
