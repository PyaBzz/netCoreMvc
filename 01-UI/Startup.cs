﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PyaFramework.Middleware;
using PyaFramework.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using myCoreMvc.App.Consuming;
using myCoreMvc.App.Providing;

namespace myCoreMvc.UI
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
            // There seems to be no way to access services outside NetCore DI container.
            // The only ways to resolve them to an instance are:
            // 1- Constructor injection
            // 2- Action Method Injection: public IActionResult Index([FromServices] ILogger logger)
            // 3- Manual injection: Use of HttpContext.RequestServices.GetService()
            // If you need to access service A in service B, you'll need to also register B in the container
            // and inject A into B's constructor.
            #endregion

            services.AddSingleton(new LoggerFactory()
                .AddConsole((category, logLevel) => category.Contains("Microsoft") == false)
                .CreateLogger("myCoreMvc"));

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            services.AddSingleton<IConfiguration>(config); // Could we bind a config object of type dynamic with all properties and children?

            services.AddSingleton(new EfCtx());
            services.AddSingleton<IDataProvider, DbMock>();
            services.AddSingleton<IUserService, UserServiceMock>();
            services.AddSingleton<IWorkPlanBiz, WorkPlanBiz>();
            services.AddSingleton<IWorkItemBiz, WorkItemBiz>();

            var authBuilder = services.AddAuthentication(options =>
                {
                    var schemeName = AuthConstants.SchemeName; // We could simply use string: "Cookies"
                    options.DefaultAuthenticateScheme = schemeName;
                    options.DefaultSignInScheme = schemeName;
                    options.DefaultChallengeScheme = schemeName;
                });

            authBuilder.AddCookie(options =>
                {
                    options.LoginPath = "/LogIn/LogIn/SignIn"; //Task: Replace hardcoded values. Search for "path" to find similar instances
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

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("JustADummyEntryHereShutTheViewEngineUp" + RazorViewEngine.ViewExtension);

                o.AreaViewLocationFormats.Clear();
                o.AreaViewLocationFormats.Add("/Pages/{2}/{1}/View/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Pages/{2}/View/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Pages/zzShared/{0}" + RazorViewEngine.ViewExtension);
            });
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

            appBuilder.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{area=WorkItems}/{controller=WorkItemList}/{action=Index}/{id?}");
            });
        }
    }
}