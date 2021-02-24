using Microsoft.Extensions.DependencyInjection;
using myCoreMvc.App.Services;

namespace myCoreMvc.App.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<Config>(ConfigFactory.Get());

            // services.AddTransient<IDataRepo, DataRepoMock>();
            services.AddTransient<IDataRepo, DataRepo>();
        }
    }
}
