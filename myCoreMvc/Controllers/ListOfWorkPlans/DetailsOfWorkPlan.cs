using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc
{
    public class DetailsOfWorkPlan : Controller
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
                case DataProvider.TransactionResult.NotFound: result = "Found no WorkPlan with the provided Id."; break;
                case DataProvider.TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkPlan with the provided Id."; break;
            }
            return RedirectToAction("Index", "ListOfWorkPlans", new { message = result });  // Prevents form re-submission by refresh
        }
    }
}
