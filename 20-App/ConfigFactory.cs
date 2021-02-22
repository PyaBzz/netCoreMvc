using System.IO;
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
                IConfiguration iConfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("settings.json")
                        .Build();
                iConfig.Bind(config);
            }
            return config;
        }
    }
}
