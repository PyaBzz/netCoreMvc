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

namespace myCoreMvc.App
{
    public class Config
    {
        public string ConnectionString { get; set; }
        public string InitScriptRelPath { get; set; }
        public string DestroyScriptRelPath { get; set; }
        public bool ShowNavToUnknownUsers { get; set; }
        public AuthConfig Authentication { get; set; }
    }

    public class AuthConfig
    {
        public string CookieName { get; set; }
        public int SessionLifeTime { get; set; }
    }
}
