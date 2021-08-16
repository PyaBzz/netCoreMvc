using Microsoft.Extensions.DependencyInjection;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using myCoreMvc.App.Services;
using myCoreMvc.Persistence;

namespace myCoreMvc.Persistence.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Config>(ConfigFactory.Get());
            services.AddTransient<IDbConFactory, DbTestConFactory>();
            services.AddTransient<IHashFactory, HashFactory>();
            // services.AddTransient<IUserRepo, UserRepoMock>();
            services.AddTransient<IUserRepo, UserRepo>();
            // services.AddTransient<IWorkItemRepo, WorkItemRepoMock>();
            services.AddTransient<IWorkItemRepo, WorkItemRepo>();

            // services.AddTransient<IDataRepo, DataRepoMock>();
            // services.AddTransient<IDataRepo, DataRepo>();

            // services.AddTransient<IWorkplanRepo, WorkplanRepoMock>();
            // services.AddTransient<IWorkplanRepo, WorkplanRepo>();
        }
    }
}
