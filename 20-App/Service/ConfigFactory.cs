using System.IO;
using Microsoft.Extensions.Configuration;

namespace myCoreMvc.App.Services
{
    public static class ConfigFactory
    {
        private static Config product;

        public static Config Get()
        {
            if (product == null)
            {
                product = new Config();
                IConfiguration iConfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("settings.json")
                        .Build();
                iConfig.Bind(product);
            }
            return product;
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
        public bool ShowNavigationToUnauthenticatedUsers { get; set; }
        public AuthConfig Authentication { get; set; }
    }

    public class AuthConfig
    {
        public string CookieName { get; set; }
        public int SessionLifeTime { get; set; }
    }
}
