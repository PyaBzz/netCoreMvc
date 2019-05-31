using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PyaFramework.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using myCoreMvc.App.Providing;
using PyaFramework.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("LogIn")]
    public class LogInController : BaseController
    {
        private readonly IUserBiz UserBiz;

        public LogInController(IUserBiz userBiz)
        {
            UserBiz = userBiz;
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
                if (await UserBiz.GetPrincipal(model.UserName, model.PassWord, out ClaimsPrincipal claimsPrincipal))
                {
                    var expiryTime = DateTime.UtcNow.AddSeconds(config.GetValue<int>("AuthenticationSessionLifeTime"));

                    await HttpContext.SignInAsync(AuthConstants.SchemeName, claimsPrincipal);
                    if (returnUrl != null) return Redirect(returnUrl);
                    return RedirectToAction(nameof(WorkItemListController.Index), Short<WorkItemListController>.Name, new { area = "WorkItems", message = "You're in!" });
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
