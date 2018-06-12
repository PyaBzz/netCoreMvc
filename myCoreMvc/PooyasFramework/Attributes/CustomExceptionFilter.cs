using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using myCoreMvc;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PooyasFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //public CustomExceptionFilterAttribute()
        //{
        //    //Constructor stuff
        //}

        public override void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult { Content = "From custom handler" };
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
        }
    }
}
