using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using myCoreMvc.App;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.Controllers
{
    [Area("LogIn")]
    public class LogInController : BaseController
    {
        private IUserService _userService;

        public LogInController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult SignIn()
        {
            return View(new EnterModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(EnterModel model, string returnUrl, [FromServices] IConfiguration config)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.GetPrincipal(model.UserName, model.PassWord, out ClaimsPrincipal claimsPrincipal))
                {
                    var expiryTime = DateTime.UtcNow.AddSeconds(config.GetValue<int>("AuthenticationSessionLifeTime"));

                    await HttpContext.SignInAsync(AuthConstants.SchemeName, claimsPrincipal);
                    if (returnUrl != null) return Redirect(returnUrl);
                    return RedirectToAction(nameof(WorkItemListController.Index), ShortNameOf<WorkItemListController>(), new { area = "WorkItems", message = "You're in!" });
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(AuthConstants.SchemeName);
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult Denied()
        {
            return View("MessageOnly", "Access Denied!");
        }

        public class EnterModel
        {
            [Required(ErrorMessage = "{0} is required")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "{0} is required")]
            public string PassWord { get; set; }
        }
    }
}
