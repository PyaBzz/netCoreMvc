using System;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using myCoreMvc.Models;
using PooyasFramework;
using PooyasFramework.IoC;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : BaseController
    {
        public IActionResult Logger([FromServices] ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger("Logger");
            var message = $"Dasoo at {DateTime.Now.ToLongTimeString()}";
            logger.LogCritical(message);
            return View("~/Views/Shared/MessageOnly.cshtml", $"Find \"{message}\" in your Web server terminal.");
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
            var instance = Db.Get<WorkItem>(5);
            return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", instance);
        }
    }
}
