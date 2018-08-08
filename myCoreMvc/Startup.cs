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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;

namespace myCoreMvc
{
    public class Startup
    {
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Lesson: I don't put this service in the default IoC container because:
            #region
            // There seems to be no way to make this service available outside MVC controllers.
            // The only ways to resolve them to an instance are:
            // 1- Constructor injection
            // 2- Action Method Injection: public IActionResult Index([FromServices] ILogger logger)
            // 3- Manual injection: Use of HttpContext.RequestServices.GetService()
            // All of which exist only in controllers. Further information is at the middle of this page: https://stackify.com/net-core-loggerfactory-use-correctly/
            #endregion
            ServiceInjector.Register<IDataProvider, DbMock>(Injection.Singleton);

            services.AddSingleton(new LoggerFactory()
                .AddConsole((category, logLevel) => category.Contains("Microsoft") == false)
                .CreateLogger("myCoreMvc"));

            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build());

            services.AddSingleton<IUserService>(new UserServiceMock(new Dictionary<string, string> { { "Hasang", "Palang" } }));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }).AddCookie(options => { options.LoginPath = "/signin"; });
        }

        //TODO: Enable SSH based on:
        // the introduction video of this course: https://app.pluralsight.com/player?course=aspnet-core-identity-management-playbook&author=chris-klug&name=aspnet-core-identity-management-playbook-m2&clip=1&mode=live

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IHostingEnvironment env, IApplicationBuilder appBuilder)
        {
            //Task: Make use of this.
            if (env.IsDevelopment()) appBuilder.UseDeveloperExceptionPage();

            appBuilder.UseMiddleware<CustomMiddleware>();

            var staticFileOptions = new StaticFileOptions();
            staticFileOptions.RequestPath = "/StaticContent";
            var path = Path.Combine(env.ContentRootPath, "StaticFiles");
            staticFileOptions.FileProvider = new PhysicalFileProvider(path);
            appBuilder.UseStaticFiles(staticFileOptions);

            appBuilder.UseAuthentication();

            //Experience: When this is put before Authentication middleware it doesn't work. Why?
            appBuilder.UseMiddleware<AntiForgeryTokenValidatorMiddleware>();

            appBuilder.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=ListOfWorkItems}/{action=Index}/{id?}"); });
        }
    }
}
