using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PyaFramework.Core;
using myCoreMvc.App;
using myCoreMvc.App.Providing;
using System.ComponentModel.DataAnnotations;
using PyaFramework.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("Users")]
    public class UserSetPasswordController : BaseController
    {
        private readonly IUserBiz UserBiz;

        public UserSetPasswordController(IUserBiz userBiz)
            => UserBiz = userBiz;

        public IActionResult Index(Guid id)
        {
            var user = UserBiz.Get(id);
            var inputModel = new EnterModel();
            if (user != null)
                inputModel.CopySimilarPropertiesFrom(user);
            return View("UserSetPassword", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserBiz.Get(inputModel.Id);
                var transactionResult = UserBiz.Of(user).SetPassword(inputModel.Password);
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Password updated"; break;
                    case TransactionResult.NotFound: resultMessage = "User not found"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(UserListController.Index), Short<UserListController>.Name, new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("UserSetPassword", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            [Required]
            public Guid Id { get; set; }
            public string Name { get; set; }
            [Required]
            public string Password { get; set; }
            public string Message = "";
        }
    }
}
