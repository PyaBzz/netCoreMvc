using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PooyasFramework.Middleware;
using PooyasFramework;
using myCoreMvc.Services;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace myCoreMvc
{
    public class Startup
    {
        // This method gets called by the runtime.
        // Use it to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(new RequireHttpsAttribute()); });
            ServiceInjector.Register<IDataProvider, DbMock>(Injection.Singleton); //TODO: Why don't we use "services"?
            var users = new Dictionary<string, string> { { "Hasang", "Palang" } };
            services.AddSingleton<IUserService>(new UserServiceMock(users));
        }

        //TODO: Experiment with cookies.
        //TODO: Implement user authentication with cookies: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-2.1&tabs=aspnetcore2x
        //TODO: Use OnActionExecuting() and OnActionExecuted() methods from here: https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controller.onactionexecuting?view=aspnetcore-2.1
        //TODO: Enable SSH based on the introduction video of this course: https://app.pluralsight.com/player?course=aspnet-core-identity-management-playbook&author=chris-klug&name=aspnet-core-identity-management-playbook-m2&clip=1&mode=live

        // This method gets called by the runtime.
        // Use it to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMiddleware<CustomMiddleware>();

            app.UseMiddleware<AntiForgeryTokenValidatorMiddleware>();

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44383));

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            var staticFileOptions = new StaticFileOptions();
            staticFileOptions.RequestPath = "/StaticContent";
            var path = Path.Combine(env.ContentRootPath, "StaticFiles");
            staticFileOptions.FileProvider = new PhysicalFileProvider(path);
            app.UseStaticFiles(staticFileOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ListOfWorkItems}/{action=Index}/{id?}");
            });
        }
    }
}
