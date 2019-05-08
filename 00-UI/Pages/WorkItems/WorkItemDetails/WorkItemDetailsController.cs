using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;

namespace myCoreMvc.Controllers
{
    [Area("WorkItems")]
    public class WorkItemDetailsController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkItem>(id);
            return View("WorkItemDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (DataProvider.Delete<WorkItem>(id))
            {
                case TransactionResult.NotFound: result = "Found no WorkItem with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkItem with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkItemListController.Index), ShortNameOf<WorkItemListController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
