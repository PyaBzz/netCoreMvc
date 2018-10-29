using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using PooyasFramework;
using System;

namespace myCoreMvc.Controllers
{
    public class DetailsOfWorkItemController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkItem>(id);
            return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", viewModel);
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
            return RedirectToAction(nameof(ListOfWorkItemsController.Index), ShortNameOf<ListOfWorkItemsController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
