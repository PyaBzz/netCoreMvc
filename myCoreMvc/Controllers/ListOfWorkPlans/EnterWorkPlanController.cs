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
    public class EnterWorkPlanController : Controller
    {
        public IActionResult Index(Guid id)
        {
            var plan = DataProvider.Get<WorkPlan>(wp => wp.Id == id);
            var inputModel = new EnterModel();
            if (plan != null) inputModel.CopySimilarPropertiesFrom(plan);
            return View("~/Views/ListOfWorkPlans/EnterWorkPlan.cshtml", inputModel);  // TODO: Use "asp-" tag helpers instead of tags attributes.
                                                                                      // TODO: See if you can minimise duplicate markup in the view.
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var workPlan = new WorkPlan();
                workPlan.CopySimilarPropertiesFrom(inputModel);  // We use this simple way to prevent malicious over-posting
                var result = "";
                switch (DataProvider.Save(workPlan))
                {
                    case DataProvider.TransactionResult.Updated: result = "Plan updated"; break;
                    case DataProvider.TransactionResult.Added: result = "New plan added"; break;
                    default: result = "New plan added"; break;
                }
                return RedirectToAction("Index", "ListOfWorkPlans", new { message = result });  // Prevents form re-submission by refresh
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
            [Display(Name = "Plan name"), Required]
            public string Name { get; set; }
            public string Message = "";
        }
    }
}
