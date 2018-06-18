using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PooyasFramework.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate nextDelegate;

        public CustomMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Stuff that happen BEFORE the next middleware in the pipeline
            //await context.Response.WriteAsync("Forward order- In the custom middleware class" + Environment.NewLine);

            await nextDelegate.Invoke(context); // This invokes the next middleware in the pipeline

            // Stuff that happen AFTER the next middleware in the pipeline
            //await context.Response.WriteAsync("Reverse order- In the custom middleware class" + Environment.NewLine);
        }
    }
}
