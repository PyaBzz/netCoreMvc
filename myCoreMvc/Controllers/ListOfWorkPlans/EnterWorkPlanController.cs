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
            return View("~/Views/ListOfWorkPlans/EnterWorkPlan.cshtml", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var workPlan = new WorkPlan();
                workPlan.CopySimilarPropertiesFrom(inputModel);  // We use this simple way to prevent malicious over-posting
                DataProvider.TransactionResult transactionResult;
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
                    case DataProvider.TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case DataProvider.TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction("Index", "ListOfWorkPlans", new { message = resultMessage });  // Prevents form re-submission by refresh
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
