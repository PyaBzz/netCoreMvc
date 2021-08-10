﻿using System;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Baz.Core;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanEnterController : BaseController
    {
        private readonly IWorkplanRepo WorkPlanRepo;

        public WorkPlanEnterController(IWorkplanRepo repo)
            => WorkPlanRepo = repo;

        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel();
            if (id != Guid.Empty)
            {
                var workPlan = WorkPlanRepo.Get(id);
                if (workPlan != null) inputModel.CopySimilarPropertiesFrom(workPlan);
            }
            return View("WorkPlanEnter", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            throw new NotImplementedException();
            // if (ModelState.IsValid)
            // {
            //     var workPlan = inputModel.Id == Guid.Empty
            //         ? new WorkPlan()
            //         : WorkPlanRepo.Get(inputModel.Id);  //Task: Instead of finding the object again, cache it in the view model as inputModel.Item

            //     workPlan.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
            //     var transactionResult = WorkPlanRepo.Add(workPlan);

            //     string resultMessage;
            //     switch (transactionResult)
            //     {
            //         case TransactionResult.Updated: resultMessage = "Item updated"; break;
            //         case TransactionResult.Added: resultMessage = "New item added"; break;
            //         default: resultMessage = transactionResult.ToString(); break;
            //     }

            //     return RedirectToAction(nameof(WorkPlanListController.Index), Short<WorkPlanListController>.Name, new { message = resultMessage });  // Prevents re-submission by refresh
            // }
            // else
            // {
            //     inputModel.Message = "Invalid values for: "
            //         + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
            //     return View("WorkPlanEnter", inputModel);
            // }
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
