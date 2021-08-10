using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Baz.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using myCoreMvc.UI.Controllers;
using myCoreMvc.App;

namespace myCoreMvc.UI
{
    public partial class Startup
    {
        //##################################################################
        //###                          Services                          ###
        //##################################################################

        public void ConfigureServices(IServiceCollection services)
        {
            #region Lesson
            // There seems to be no way to access services outside NetCore DI container.
            // The only ways to resolve them to an instance are:
            // 1- Constructor injection
            // 2- Action Method Injection: public IActionResult Index([FromServices] ILogger logger)
            // 3- Manual injection: Use of HttpContext.RequestServices.GetService()
            // If you need to access service A in service B, you'll need to also register B in the container
            // and inject A into B's constructor.
            #endregion

            services.AddLogging(iLoggingBuilder =>
            {
                iLoggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                iLoggingBuilder.AddConsole();
            });

            var config = ConfigFactory.Get();

            services.AddSingleton<Config>(config);

            // services.AddSingleton(new EfCtx());
            // services.AddSingleton<IDataRepo, DataRepo>();
            services.AddSingleton<IUserBiz, UserBiz>();
            services.AddSingleton<IWorkplanRepo, WorkplanRepoMock>();
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
                    options.LoginPath = $"/{AreaOf<LogInController>.Name}/{Short<LogInController>.Name}/{nameof(LogInController.SignIn)}";
                    options.AccessDeniedPath = $"/{AreaOf<LogInController>.Name}/{Short<LogInController>.Name}/{nameof(LogInController.Denied)}";
                    options.Cookie.Name = config.Authentication.CookieName;
                    options.Cookie.MaxAge = TimeSpan.FromSeconds(config.Authentication.SessionLifeTime);
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
                o.AreaViewLocationFormats.Add("/Pages/{2}/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Pages/{2}/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Pages/zzShared/{0}" + RazorViewEngine.ViewExtension);
            });
        }
    }
}
