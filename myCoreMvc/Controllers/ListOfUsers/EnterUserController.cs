using System;
using System.Linq;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;

namespace myCoreMvc.Controllers
{
    public class EnterUserController : BaseController
    {
        public IActionResult Index(Guid id)
        { //Task: Complete the CRUD for user management
            var user = DataProvider.Get<User>(wp => wp.Id == id);
            var inputModel = new EnterModel();
            if (user != null) inputModel.CopySimilarPropertiesFrom(user);
            return View("~/Views/ListOfUsers/EnterUser.cshtml", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User();
                user.CopySimilarPropertiesFrom(inputModel);  // We use this simple way to prevent malicious over-posting
                TransactionResult transactionResult;
                if (user.Id == Guid.Empty)
                {
                    transactionResult = DataProvider.Add(user);
                }
                else
                {
                    transactionResult = DataProvider.Update(user);
                }
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

            public string Message = "";
        }
    }
}
