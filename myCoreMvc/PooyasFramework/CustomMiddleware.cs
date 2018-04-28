using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public class CustomMiddleware
    {
        private RequestDelegate nextDelegate;
        public CustomMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("Forward order- In the custom middleware class" + Environment.NewLine);
            await nextDelegate.Invoke(context);
            await context.Response.WriteAsync("Reverse order- In the custom middleware class" + Environment.NewLine);
        }
    }
}
