using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        public async Task<IActionResult> SignIn(EnterModel model)
        {
            if (ModelState.IsValid)
            {
                User user;
                if (await _userService.ValidateCredentials(model.userName, model.passWord, out user))
                {
                    return RedirectToAction("Index", "ListOfWorkItems", new { message = "You're in!" });
                }
            }
            return View(model);
        }

        public class EnterModel
        {
            [Required(ErrorMessage = "UserName is required")]
            public string userName { get; set; }
            [Required(ErrorMessage = "PassWord is required")]
            public string passWord { get; set; }
        }
    }
}
