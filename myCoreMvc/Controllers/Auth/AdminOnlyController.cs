using System;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using myCoreMvc.Models;
using PooyasFramework;
using PooyasFramework.IoC;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace myCoreMvc.Controllers
{
    //Task: Can we use "AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme" as a parameter to the Authorize attribute to choose among available schemes?
    [Authorize(Policy = "adminOnly")]
    public class AdminOnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Views/Shared/MessageOnly.cshtml", ">>> Admin <<<");
        }
    }
}
