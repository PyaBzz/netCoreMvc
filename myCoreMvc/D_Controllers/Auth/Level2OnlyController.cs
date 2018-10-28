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
    //Task: Can we use "AuthenticationSchemes = AuthConstants.AuthSchemeName" as a parameter to the Authorize attribute to choose among available schemes?
    [Authorize(Policy = AuthConstants.Level2PolicyName)]
    public class Level2OnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("~/Views/Shared/MessageOnly.cshtml", $">>> Access {AuthConstants.Level2PolicyName} <<<");
        }
    }
}
