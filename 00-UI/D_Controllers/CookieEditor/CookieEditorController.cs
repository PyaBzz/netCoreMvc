using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace myCoreMvc.Controllers
{
    public class CookieEditorController : BaseController
    {
        public IActionResult Index()
        {
            return View("CookieEditor");
        }

        [HttpPost]
        public IActionResult Delete(string key)
        {
            Response.Cookies.Delete(key);
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }

        [HttpPost]
        public IActionResult Add(string key, string value, string LifeTime)
        {
            int lifeTimeInSeconds;
            if (int.TryParse(LifeTime, out lifeTimeInSeconds))
            {
                var lifeTime = TimeSpan.FromSeconds(lifeTimeInSeconds);
                Response.Cookies.Append(key, value, new CookieOptions { MaxAge = lifeTime });
            }
            else
            {
                Response.Cookies.Append(key, value);
            }
            return RedirectToAction(nameof(CookieEditorController.Index), ShortNameOf<CookieEditorController>());
        }
    }
}
