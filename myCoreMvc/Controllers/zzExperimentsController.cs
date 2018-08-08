using System;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using myCoreMvc.Models;
using PooyasFramework;
using PooyasFramework.IoC;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : BaseController
    {
        public IActionResult Logger([FromServices] ILogger logger)
        {
            var message = $"Dasoo at {DateTime.Now.ToLongTimeString()}";
            logger.LogCritical(message);
            return View("~/Views/Shared/MessageOnly.cshtml", $"Find \"{message}\" in your Web server terminal.");
        }

        public IActionResult Config([FromServices] IConfiguration config)
        {
            var keys = config.AsEnumerable().ToDictionary(e => e.Key, e => e.Value).Keys;
            var message = "The key-value pairs in the config file are";
            foreach (var key in keys) message += $"{Environment.NewLine}{key} : {config[key]}";
            return View("~/Views/Shared/MessageOnly.cshtml", message);
        }

        public IActionResult CookieEditor()
        {
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }

        public IActionResult Delegate()
        {
            var result = string.Empty;
            DelegateType hasang;
            hasang = Delegates.DelegateImplementation1;
            result += hasang("Dasoo") + Environment.NewLine;
            hasang -= Delegates.DelegateImplementation1;
            hasang += Delegates.DelegateImplementation2;
            result += hasang("Dasoo");
            return View("~/Views/Shared/MessageOnly.cshtml", result);
        }

        public ActionResult Database()
        {
            //var instance = Db.Get<WorkItem>(5);
            //return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", instance);
            return View("~/Views/Shared/MessageOnly.cshtml", $"This method needs correction!{Environment.NewLine}It's temporarily down.");
        }
    }
}
