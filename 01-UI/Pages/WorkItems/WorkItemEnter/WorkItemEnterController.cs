using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PyaFramework.Core;
using PyaFramework.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using myCoreMvc.App;
using myCoreMvc.App.Providing;
using PyaFramework.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkItems")]
    public class WorkItemEnterController : BaseController
    {
        private readonly IWorkItemBiz WorkItemBiz;
        private readonly IWorkPlanBiz WorkPlanBiz;

        public WorkItemEnterController(IWorkItemBiz workItemBiz, IWorkPlanBiz workPlanBiz)
        {
            WorkItemBiz = workItemBiz;
            WorkPlanBiz = workPlanBiz;
        }

        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel();
            inputModel.PriorityChoices = WorkItem.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == inputModel.Priority });
            inputModel.WorkPlanChoices = WorkPlanBiz.GetList().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == inputModel.WorkPlan });
            if (id != Guid.Empty)
            {
                var item = WorkItemBiz.Get(id);
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
                //Task: What are these lines of comment?!
                // ModelState.AddModelError("Reference", "It must be in blabla format!")
                // ModelState.AddModelError("", "This is an object level error rather than property level.")
                // @Html.ValidationSummary(true)
                // @Html.ValidationMessageFor(p => p.Reference)
                inputModel.PriorityChoices = WorkItem.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == inputModel.Priority });
                inputModel.WorkPlanChoices = WorkPlanBiz.GetList().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == inputModel.WorkPlan });
                WorkItem workItem;
                TransactionResult transactionResult;
                if (inputModel.Id == Guid.Empty)
                {
                    workItem = new WorkItem();
                    workItem.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                    workItem.WorkPlan = WorkPlanBiz.Get(inputModel.WorkPlan);
                    transactionResult = WorkItemBiz.Of(workItem).Add();
                }
                else
                {
                    workItem = WorkItemBiz.Get(inputModel.Id);
                    workItem.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                    transactionResult = WorkItemBiz.Of(workItem).Update();
                }
                var resultMessage = "";
                switch (transactionResult)
                {
                    case TransactionResult.Updated: resultMessage = "Item updated"; break;
                    case TransactionResult.Added: resultMessage = "New item added"; break;
                    default: resultMessage = transactionResult.ToString(); break;
                }
                return RedirectToAction(nameof(WorkItemListController.Index), ShortName.Of<WorkItemListController>(), new { message = resultMessage });  // Prevents re-submission by refresh
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
            public Guid Id { get; set; }
            [Display(Name = "Item name")]
            [ValidateAlphanumeric(3, 16)]
            public string Name { get; set; }
            public Guid WorkPlan { get; set; }
            public string Reference { get; set; }
            public int Priority { get; set; }
            public IEnumerable<SelectListItem> PriorityChoices { get; set; }
            public IEnumerable<SelectListItem> WorkPlanChoices { get; set; }
            public string Message = "";
        }
    }
}
//Lesson: View models should be simpe data containers.
//They should contain no behavior and neither expose nor hold any dependencies.
//This forces the Razor views to be simple and stupid and hence most maintainable.
//When you follow that practice, only the controller will (and should) have dependencies.
//Then the controller should set necessary properties on the VM
//This may require filtering or policy enforcement based on business requirements none of which is a concern of VM!
