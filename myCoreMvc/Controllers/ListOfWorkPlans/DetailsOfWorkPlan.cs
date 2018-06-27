using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using myCoreMvc.PooyasFramework;
using myCoreMvc.Services;
using System;

namespace myCoreMvc
{
    public class DetailsOfWorkPlan : BaseController
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
            return RedirectToAction("Index", "ListOfWorkPlans", new { message = result });  // Prevents form re-submission by refresh
        }
    }
}
