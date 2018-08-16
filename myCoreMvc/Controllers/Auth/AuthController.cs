using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using myCoreMvc.Models;
using PooyasFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.Controllers
{
    public class AuthController : BaseController
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
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

                    await HttpContext.SignInAsync("Cookies", claimsPrincipal);
                    if (returnUrl != null) return Redirect(returnUrl);
                    return RedirectToAction(nameof(ListOfWorkItemsController.Index), ShortNameOf<ListOfWorkItemsController>(), new { message = "You're in!" });
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult Denied()
        {
            return View("~/Views/Shared/MessageOnly.cshtml", "Access Denied!");
        }

        public class EnterModel
        {
            [Required(ErrorMessage = "UserName is required")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "PassWord is required")]
            public string PassWord { get; set; }
        }
    }
}
