using System;
using System.Linq;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;

namespace myCoreMvc.Controllers
{
    public class EnterWorkPlanController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel();
            if (id != Guid.Empty)
            {
                var workPlan = DataProvider.Get<WorkPlan>(id);
                if (workPlan != null) inputModel.CopySimilarPropertiesFrom(workPlan);
            }
            return View("~/Views/ListOfWorkPlans/EnterWorkPlan.cshtml", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var workPlan = new WorkPlan();
                workPlan.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                TransactionResult transactionResult;
                if (workPlan.Id == Guid.Empty)
                {
                    transactionResult = DataProvider.Add(workPlan);
                }
                else
                {
                    transactionResult = DataProvider.Update(workPlan);
                }
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(ListOfWorkPlansController.Index), ShortNameOf<ListOfWorkPlansController>(), new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("~/Views/ListOfWorkPlans/EnterWorkPlan.cshtml", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            public Guid Id { get; set; }

            [Display(Name = "Plan name")]
            [Required(ErrorMessage = "{0} is mandatory.")]
            [StringLength(16, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters in length.")]
            [RegularExpression("^[A-Z][a-zA-Z0-9]*", ErrorMessage = "{0} must start with a capital letter and may only contain alphanumeric characters.")]
            public string Name { get; set; }

            public string Message = "";
        }
    }
}
