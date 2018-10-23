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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using myCoreMvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Authorization;
using myCoreMvc.PooyasFramework.Filters;

namespace myCoreMvc
{
    public class Startup
    {
        //##################################################################
        //###                          Services                          ###
        //##################################################################

        public void ConfigureServices(IServiceCollection services)
        {
            //Lesson:
            #region
            // I don't put this service in the default IoC container because:
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

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            services.AddSingleton<IConfiguration>(config); // Could we bind a config object of type dynamic with all properties and children?

            services.AddSingleton<IUserService>(new UserServiceMock());

            var authBuilder = services.AddAuthentication(options =>
                {
                    var schemeName = AuthConstants.SchemeName; // We could simply use string: "Cookies"
                    options.DefaultAuthenticateScheme = schemeName;
                    options.DefaultSignInScheme = schemeName;
                    options.DefaultChallengeScheme = schemeName;
                });

            authBuilder.AddCookie(options =>
                {
                    options.LoginPath = "/auth/signin"; //Task: Replace hardcoded values. Search for "path" to find similar instances
                    options.AccessDeniedPath = "/auth/denied";
                    options.Cookie.Name = config.GetSection("Authentication").GetValue<string>("CookieName");
                    options.Cookie.MaxAge = TimeSpan.FromSeconds(config.GetSection("Authentication").GetValue<int>("AuthenticationSessionLifeTime"));
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(AuthConstants.SchemeName)
                .RequireAuthenticatedUser().Build();

                // Access Level to Role mapping
                options.AddPolicy(AuthConstants.Level0PolicyName, policy =>
                { // Equivalent to un-authenticated users
                    policy.RequireAssertion(ctx => true);
                });
                options.AddPolicy(AuthConstants.Level1PolicyName, policy =>
                { // Equivalent to the default [Authorize] attribute
                    policy.RequireRole(new[] { AuthConstants.JuniorRoleName, AuthConstants.SeniorRoleName, AuthConstants.AdminRoleName });
                });
                options.AddPolicy(AuthConstants.Level2PolicyName, policy =>
                {
                    policy.RequireRole(new[] { AuthConstants.SeniorRoleName, AuthConstants.AdminRoleName });
                });
                options.AddPolicy(AuthConstants.Level3PolicyName, policy =>
                {
                    policy.RequireRole(new[] { AuthConstants.AdminRoleName });
                });
            });

            services.AddMvc(
                options => { options.Conventions.Add(new AddAuthoriseAttributeConvention()); }
                );
        }

        //##################################################################
        //###                          Pipeline                          ###
        //##################################################################

        public void Configure(IHostingEnvironment env, IApplicationBuilder appBuilder)
        {
            //Lesson:
            #region
            // The "IsDevelopment" method returns true only if EnvironmentName equals EnvironmentName.Development (string)
            // Our environment is currently called "DevProj"
            //if (env.IsDevelopment()) appBuilder.UseDeveloperExceptionPage();
            #endregion
            appBuilder.UseDeveloperExceptionPage();

            appBuilder.UseMiddleware<CustomMiddleware>();

            //TODO: Redirect to SSH using this:
            //appBuilder.UseRewriter(new Microsoft.AspNetCore.Rewrite.RewriteOptions().AddRedirectToHttpsPermanent());

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
