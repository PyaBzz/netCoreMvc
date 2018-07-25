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
            var inputModel = new EnterModel();
            inputModel.Cookies = Request.Cookies;
            return View("~/Views/CookieEditor/CookieEditor.cshtml", inputModel);
        }

        [HttpPost]
        public IActionResult Index(List<KeyValuePair<string, string>> inputModel)
        {
            foreach (var key in Request.Form.Keys)
            {
                //Do something
            }
            return Content(inputModel.Select(i => i.Key).ToString(" | "));
        }

        public ContentResult Add()
        {
            var key = $"Key{Request.Cookies.Count()}";
            var value = $"Value{Request.Cookies.Count()}";
            var content = "Request Cookies:" + Environment.NewLine;
            content += Request.Cookies.ToString(Environment.NewLine) + Environment.NewLine;
            content += "=======================================================================" + Environment.NewLine;
            content += $"Adding [{key}, {value}]";

            Response.Cookies.Append(key, value);
            return Content(content);
        }

        public class EnterModel : IClonable
        {
            public IRequestCookieCollection Cookies { get; set; }
        }
    }
}
