using System.IO;
using System.Reflection;
using Baz.Core;
using Microsoft.Extensions.Configuration;

namespace myCoreMvc.App
{
    public static class ConfigFactory
    {
        private static Config config;

        public static Config Get()
        {
            if (config == null)
            {
                config = new Config();
                var outputDir = Assembly.GetExecutingAssembly().GetDirectory();
                var filePath = Path.Combine(outputDir, "settings.json");
                IConfiguration iConfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(filePath)
                        .Build();
                iConfig.Bind(config);
            }
            return config;
        }
    }
}
