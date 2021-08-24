using Microsoft.Extensions.DependencyInjection;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using myCoreMvc.App.Services;

namespace myCoreMvc.Persistence.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Config>(ConfigFactory.Get());
            services.AddTransient<IDbConFactory, DbTestConFactory>();
            services.AddTransient<IHashFactory, HashFactory>();

            services.AddTransient<CrudRepo<DummyA>, DummyARepo>();
            // services.AddTransient<IUserRepo, UserRepoMock>();
            services.AddTransient<IUserRepo, UserRepo>();
            // services.AddTransient<IOrderRepo, OrderRepoMock>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            // services.AddTransient<IProductRepo, ProductRepoMock>();
            services.AddTransient<IProductRepo, ProductRepo>();
        }
    }
}
