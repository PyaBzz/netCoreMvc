using System;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using myCoreMvc.Domain;
using PyaFramework.Core;
using PyaFramework.IoC;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace myCoreMvc.Controllers
{
    [Authorize]
    [Area("Level1Only")]
    public class Level1OnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("MessageOnly", $">>> Access {AuthConstants.Level1PolicyName} (Only Authenticated) <<<");
        }
    }
}
