using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace myCoreMvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default", 
                    template: "{controller=ListOfWorkItems}/{action=Index}/{id?}");
            });

            //-------------------------------  Pipeline Experiment  ---------------------------------
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Forward order- In the Startup class" + Environment.NewLine);
            //    await next.Invoke();
            //    await context.Response.WriteAsync("Reverse order- In the Startup class" + Environment.NewLine);
            //});
            //app.UseMiddleware<CustomMiddleware>();
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("--------------------------------------------------------------" + Environment.NewLine);
            //    await context.Response.WriteAsync(
            //        "Client IP address: " + context.Request.HttpContext.Connection.RemoteIpAddress.ToString() +
            //        "Host IP address: " + context.Request.HttpContext.Connection.LocalIpAddress.ToString() + Environment.NewLine +
            //        $"8 factorial is {8.Factorial()}" + Environment.NewLine);
            //    await context.Response.WriteAsync("--------------------------------------------------------------" + Environment.NewLine);
            //});
        }
    }
}
