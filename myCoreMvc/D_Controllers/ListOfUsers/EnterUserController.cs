using System;
using System.Linq;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace myCoreMvc.Controllers
{
    public class EnterUserController : BaseController
    {
        private IUserService _userService;

        public EnterUserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(Guid id)
        {
            var user = DataProvider.Get<User>(id);
            var enterModel = new EnterModel();
            if (user != null) enterModel.CopySimilarPropertiesFrom(user);
            return View("~/Views/ListOfUsers/EnterUser.cshtml", enterModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                var transactionResult = _userService.Save(user);
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(ListOfUsersController.Index), ShortNameOf<ListOfUsersController>(), new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("~/Views/ListOfUsers/EnterUser.cshtml", inputModel);
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
            public IEnumerable<SelectListItem> RoleChoices => AuthConstants.All.Select(c => new SelectListItem { Text = c, Value = c, Selected = c == Role });
            public string Message = "";
        }
    }
}
