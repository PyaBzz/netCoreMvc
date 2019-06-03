﻿using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using PyaFramework.Middleware;

namespace myCoreMvc.UI
{
    public partial class Startup
    {
        //##################################################################
        //###                          Pipeline                          ###
        //##################################################################

        public void Configure(IHostingEnvironment env, IApplicationBuilder appBuilder)
        {
            #region Lesson
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