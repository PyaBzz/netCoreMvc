using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;

namespace myCoreMvc
{
    public class EnterWorkItemController : Controller
    {
        public IActionResult Index(Guid id)
        {
            var item = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            var inputModel = new EnterModel();
            if (item != null) inputModel.CopySimilarPropertiesFrom(item);
            return View("~/Views/ListOfWorkItems/EnterWorkItem.cshtml", inputModel);  // TODO: Use "asp-" tag helpers instead of tags attributes.
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var workItem = new WorkItem();
                // TODO: Look into object mapping solutions like AutoMapper to learn best practices.
                // ModelState.AddModelError("Reference", "It must be in blabla format!")
                // ModelState.AddModelError("", "This is an object level error rather than property level.")
                // TODO: Put a validation summary tag in your view:
                // @Html.ValidationSummary(true)
                // @Html.ValidationMessageFor(p => p.Reference)
                workItem.CopySimilarPropertiesFrom(inputModel);  // We use this simple way to prevent malicious over-posting
                workItem.WorkPlan = DataProvider.Get<WorkPlan>(inputModel.WorkPlan);
                DataProvider.TransactionResult transactionResult;
                if (workItem.Id == Guid.Empty)
                {
                    transactionResult = DataProvider.Add(workItem);
                }
                else
                {
                    transactionResult = DataProvider.Update(workItem);
                }
                var resultMessage = "";
                switch (transactionResult)
                {
                    case DataProvider.TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case DataProvider.TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction("Index", "ListOfWorkItems", new { message = resultMessage });  // Prevents form re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("~/Views/ListOfWorkItems/EnterWorkItem.cshtml", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            public Guid Id { get; set; }
            public Guid WorkPlan { get; set; }
            public String Reference { get; set; }
            public int Priority { get; set; }


            // TODO: Use custom validators to inherit from ValidationAttribute
            [Display(Name = "Item name")]
            [Required(ErrorMessage = "{0} is mandatory.")]
            [StringLength(5, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters in length.")]
            [RegularExpression("^[A-Z][a-zA-Z0-9]*", ErrorMessage = "{0} must start with a capital letter and may only contain alphanumeric characters.")]
            public string Name { get; set; }

            public IEnumerable<int> PriorityChoices { get { return WorkItem.PriorityChoices; } }
            public IEnumerable<WorkPlan> WorkPlanChoices { get { return DataProvider.GetList<WorkPlan>(); } }
            public string Message = "";
        }
    }
}
