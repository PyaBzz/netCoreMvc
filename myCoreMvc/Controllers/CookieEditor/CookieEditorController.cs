using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;
using PooyasFramework.Attributes;
using myCoreMvc.Services;
using Microsoft.AspNetCore.Http;

namespace myCoreMvc.Controllers
{
    public class CookieEditorController : BaseController
    {
        public IActionResult Index()
        {
            return View("~/Views/CookieEditor/CookieEditor.cshtml", Request.Cookies);
        }

        [HttpPost]
        public IActionResult Delete(string key)
        {
            Response.Cookies.Delete(key);
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }

        [HttpPost]
        public IActionResult Add(string key, string value)
        {
            Response.Cookies.Append(key, value);
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }
    }
}
