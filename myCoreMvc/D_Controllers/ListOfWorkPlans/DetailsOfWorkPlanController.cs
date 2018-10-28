using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using PooyasFramework;
using System;

namespace myCoreMvc.Controllers
{
    public class DetailsOfWorkPlanController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkPlan>(wi => wi.Id == id);
            return View("~/Views/ListOfWorkPlans/DetailsOfWorkPlan.cshtml", viewModel);
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
            return RedirectToAction(nameof(ListOfWorkPlansController.Index), ShortNameOf<ListOfWorkPlansController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
