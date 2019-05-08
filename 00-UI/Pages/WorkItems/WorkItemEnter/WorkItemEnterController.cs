using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PyaFramework.Core;
using PyaFramework.Attributes;
using myCoreMvc.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myCoreMvc.Controllers
{
    [Area("WorkItems")]
    public class WorkItemEnterController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel(DataProvider);
            if (id != Guid.Empty)
            {
                var item = DataProvider.Get<WorkItem>(id);
                if (item != null)
                {
                    inputModel.CopySimilarPropertiesFrom(item);
                    inputModel.WorkPlan = item.WorkPlan.Id; //Task: WorkPlan itself works in Get but its Guid works for POST. Find a way to cover both.
                }
            }
            return View("WorkItemEnter", inputModel);
        }

        [HttpPost]
        //[CustomExceptionFilter]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                // ModelState.AddModelError("Reference", "It must be in blabla format!")
                // ModelState.AddModelError("", "This is an object level error rather than property level.")
                // @Html.ValidationSummary(true)
                // @Html.ValidationMessageFor(p => p.Reference)
                WorkItem workItem;
                TransactionResult transactionResult;
                if (inputModel.Id == Guid.Empty)
                {
                    workItem = new WorkItem();
                    workItem.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                    workItem.WorkPlan = DataProvider.Get<WorkPlan>(inputModel.WorkPlan);
                    transactionResult = DataProvider.Add(workItem);
                }
                else
                {
                    workItem = DataProvider.Get<WorkItem>(inputModel.Id);
                    workItem.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                    transactionResult = DataProvider.Update(workItem);
                }
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(WorkItemListController.Index), ShortNameOf<WorkItemListController>(), new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("WorkItemEnter", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            private IDataProvider DataProvider;

            public EnterModel(IDataProvider dataProvider)
            {
                DataProvider = dataProvider;
            }

            public EnterModel() { }

            public Guid Id { get; set; }
            public Guid WorkPlan { get; set; }
            public String Reference { get; set; }
            public int Priority { get; set; }

            [Display(Name = "Item name")]
            [ValidateAlphanumeric(3, 16)]
            public string Name { get; set; }

            public IEnumerable<SelectListItem> PriorityChoices => WorkItem.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == Priority });
            public IEnumerable<SelectListItem> WorkPlanChoices => DataProvider?.GetList<WorkPlan>().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == WorkPlan });
            public string Message = "";
        }
    }
}
