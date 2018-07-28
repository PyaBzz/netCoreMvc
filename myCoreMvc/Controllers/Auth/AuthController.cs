using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
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

        [Route("signin")]
        public IActionResult SignIn()
        {
            return View(new EnterModel());
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(EnterModel model) //Task: Use returnUrl based on the video to redirect after signing in
        {
            if (ModelState.IsValid)
            {
                User user;
                if (await _userService.ValidateCredentials(model.UserName, model.PassWord, out user))
                {
                    await SignInUser(user.Name);
                    return RedirectToAction(nameof(ListOfWorkItemsController.Index), ShortNameOf<ListOfWorkItemsController>(), new { message = "You're in!" });
                }
            }
            return View(model);
        }

        public async Task SignInUser(string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, name),
                new Claim("name", name)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", null);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }

        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(ListOfWorkItemsController.Index), ShortNameOf<ListOfWorkItemsController>(), new { message = "You're out!" });
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
