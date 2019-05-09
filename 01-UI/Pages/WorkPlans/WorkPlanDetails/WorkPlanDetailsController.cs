using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Providing;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanDetailsController : BaseController
    {
        private readonly IWorkPlanBiz WorkPlanBiz;

        public WorkPlanDetailsController(IWorkPlanBiz workPlanBiz)
            => WorkPlanBiz = workPlanBiz;

        public IActionResult Index(Guid id)
        {
            var viewModel = WorkPlanBiz.Get(id);
            return View("WorkPlanDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (WorkPlanBiz.Delete(id))
            {
                case TransactionResult.NotFound: result = "Found no WorkPlan with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkPlan with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkPlanListController.Index), ShortNameOf<WorkPlanListController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
