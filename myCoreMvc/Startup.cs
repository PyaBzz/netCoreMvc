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

namespace myCoreMvc
{
    public class Startup
    {
        // This method gets called by the runtime.
        // Use it to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Lesson: I don't put this service in the default IoC container because:
            #region
            // I couldn't find a way to make this service available outside MVC controllers.
            // All material I found on the Web was abount injecting into controllers.
            #endregion
            ServiceInjector.Register<IDataProvider, DbMock>(Injection.Singleton);

            var users = new Dictionary<string, string> { { "Hasang", "Palang" } };
            services.AddSingleton<IUserService>(new UserServiceMock(users));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => { options.LoginPath = "/signin"; });
        }

        //TODO: Enable SSH based on:
        // the introduction video of this course: https://app.pluralsight.com/player?course=aspnet-core-identity-management-playbook&author=chris-klug&name=aspnet-core-identity-management-playbook-m2&clip=1&mode=live

        // This method gets called by the runtime.
        // Use it to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMiddleware<CustomMiddleware>();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            var staticFileOptions = new StaticFileOptions();
            staticFileOptions.RequestPath = "/StaticContent";
            var path = Path.Combine(env.ContentRootPath, "StaticFiles");
            staticFileOptions.FileProvider = new PhysicalFileProvider(path);
            app.UseStaticFiles(staticFileOptions);

            app.UseAuthentication();

            //Experience: When this is put before Authentication middleware it doesn't work. Why?
            app.UseMiddleware<AntiForgeryTokenValidatorMiddleware>();

            app.UseMvc(routes => { routes.MapRoute(name: "default", template: "{controller=ListOfWorkItems}/{action=Index}/{id?}"); });
        }
    }
}
