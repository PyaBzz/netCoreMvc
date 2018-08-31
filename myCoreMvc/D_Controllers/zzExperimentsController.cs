﻿using System;
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

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : BaseController
    {
        public ActionResult Env()
        {
            var env = HttpContext.RequestServices.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
            var message = string.Empty;
            foreach (var propInfo in env.GetType().GetProperties())
                message += $"{propInfo.Name} : {propInfo.GetValue(env)}{Environment.NewLine}";
            return View("~/Views/Shared/MessageOnly.cshtml", message);
        }

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

        public IActionResult ConfigBind([FromServices] IConfiguration config)
        {
            var workItem = new WorkItem();
            config.Bind("WorkItem", workItem);
            return View("~/Views/Shared/MessageOnly.cshtml", workItem.GetStringOfProperties());
        }

        public IActionResult CookieEditor()
        {
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }

        public IActionResult UserClaims()
        {
            if (User.Identity.IsAuthenticated == false)
                return View("~/Views/Shared/MessageOnly.cshtml", "Your are anonymous");

            var result = new List<string>();
            foreach (var identity in User.Identities)
            {
                result.Add($"========================== Identity : {identity.Name} ==========================" + Environment.NewLine);
                foreach (var claim in identity.Claims)
                {
                    result.Add($"{claim.Type}  ->  {claim.Value}");
                }
            }
            return View("~/Views/Shared/MessageOnly.cshtml", result.ToString(Environment.NewLine));
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

        public ActionResult CustomTag()
        {
            return View("~/Views/Experiments/CustomTagHelper.cshtml");
        }
    }
}