using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanDetailsController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkPlan>(id);
            return View("WorkPlanDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (DataProvider.Delete<WorkPlan>(id))
            {
                case TransactionResult.NotFound: result = "Found no WorkPlan with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkPlan with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkPlanListController.Index), ShortNameOf<WorkPlanListController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
