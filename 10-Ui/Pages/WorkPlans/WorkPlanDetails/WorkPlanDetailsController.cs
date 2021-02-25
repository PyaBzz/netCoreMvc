using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanDetailsController : BaseController
    {
        private readonly IWorkplanRepo WorkPlanRepo;

        public WorkPlanDetailsController(IWorkplanRepo repo)
            => WorkPlanRepo = repo;

        public IActionResult Index(Guid id)
        {
            var viewModel = WorkPlanRepo.Get(id);
            return View("WorkPlanDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (WorkPlanRepo.Delete(id))
            {
                case TransactionResult.NotFound: result = "Found no WorkPlan with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkPlan with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkPlanListController.Index), Short<WorkPlanListController>.Name, new { message = result });  // Prevents re-submission by refresh
        }
    }
}
