using System.IO;
using Microsoft.Extensions.Configuration;

namespace myCoreMvc.App.Services
{
    public static class ConfigFactory
    {
        private static IConfiguration instance;

        public static IConfiguration Get()
        {
            if (instance == null)
                instance = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("settings.json")
                        .Build();
            return instance;
        }
    }
}
