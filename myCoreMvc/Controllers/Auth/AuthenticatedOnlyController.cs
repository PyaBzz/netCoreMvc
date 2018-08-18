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
    [Authorize]
    public class AuthenticatedOnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Views/Shared/MessageOnly.cshtml", ">>> Authenticated <<<");
        }
    }
}
