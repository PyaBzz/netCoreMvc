using System;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PyaFramework.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using myCoreMvc.App;
using myCoreMvc.App.Providing;
using PyaFramework.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("Users")]
    public class UserEnterController : BaseController
    {
        private readonly IUserBiz UserBiz;

        public UserEnterController(IUserBiz userBiz)
            => UserBiz = userBiz;

        public IActionResult Index(Guid id)
        {
            var user = UserBiz.Get(id);
            var enterModel = new EnterModel();
            enterModel.RoleChoices = AuthConstants.All
                .Select(c => new SelectListItem { Text = c, Value = c, Selected = c == enterModel.Role });
            if (user != null) enterModel.CopySimilarPropertiesFrom(user);
            return View("UserEnter", enterModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                var transactionResult = UserBiz.Of(user).Save();
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(UserListController.Index), Short<UserListController>.Name, new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("UserEnter", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            public Guid Id { get; set; }

            [Display(Name = "User name")]
            [Required(ErrorMessage = "{0} is mandatory.")]
            [StringLength(16, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters in length.")]
            [RegularExpression("^[A-Z][a-zA-Z0-9]*", ErrorMessage = "{0} must start with a capital letter and may only contain alphanumeric characters.")]
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; } = new DateTime(1900, 01, 01);
            public string Role { get; set; }
            public IEnumerable<SelectListItem> RoleChoices;
            public string Message = "";
        }
    }
}
