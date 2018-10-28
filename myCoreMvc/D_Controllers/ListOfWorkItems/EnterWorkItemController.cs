using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PooyasFramework;
using PooyasFramework.Attributes;
using myCoreMvc.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myCoreMvc.Controllers
{
    public class EnterWorkItemController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var item = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            var inputModel = new EnterModel(DataProvider);
            if (item != null)
            {
                inputModel.CopySimilarPropertiesFrom(item);
                inputModel.WorkPlan = item.WorkPlan.Id; //Task: WorkPlan itself works in Get but its Guid works for POST. Find a way to cover both.
            }
            return View("~/Views/ListOfWorkItems/EnterWorkItem.cshtml", inputModel);
        }

        [HttpPost]
        [CustomExceptionFilter]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var workItem = new WorkItem();
                // ModelState.AddModelError("Reference", "It must be in blabla format!")
                // ModelState.AddModelError("", "This is an object level error rather than property level.")
                // @Html.ValidationSummary(true)
                // @Html.ValidationMessageFor(p => p.Reference)
                workItem.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                workItem.WorkPlan = DataProvider.Get<WorkPlan>(inputModel.WorkPlan);
                TransactionResult transactionResult;
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
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(ListOfWorkItemsController.Index), ShortNameOf<ListOfWorkItemsController>(), new { message = resultMessage });  // Prevents re-submission by refresh
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
            private IDataProvider DataProvider;

            public EnterModel(IDataProvider dataProvider)
            {
                DataProvider = dataProvider;
            }

            public Guid Id { get; set; }
            public Guid WorkPlan { get; set; }
            public String Reference { get; set; }
            public int Priority { get; set; }

            [Display(Name = "Item name")]
            [ValidateAlphanumeric(3, 16)]
            public string Name { get; set; }

            public IEnumerable<SelectListItem> PriorityChoices => WorkItem.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == Priority });
            public IEnumerable<SelectListItem> WorkPlanChoices => DataProvider.GetList<WorkPlan>().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == WorkPlan });
            public string Message = "";
        }
    }
}
