using Microsoft.Extensions.DependencyInjection;
using myCoreMvc.App.Services;

namespace myCoreMvc.App.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddSingleton<Config>(ConfigFactory.Get());
            // services.AddTransient<IDbConFactory, DbTestConFactory>();
            services.AddTransient<IHashFactory, HashFactory>();

            // services.AddTransient<IDataRepo, DataRepoMock>();
            // services.AddTransient<IDataRepo, DataRepo>();

            // services.AddTransient<IProductRepo, ProductRepoMock>();
            // services.AddTransient<IProductRepo, ProductRepo>();

            // services.AddTransient<IOrderRepo, OrderRepoMock>();
            // services.AddTransient<IOrderRepo, OrderRepo>();

            // services.AddTransient<IUserRepo, UserRepoMock>();
            // services.AddTransient<IUserRepo, UserRepo>();
        }
    }
}
