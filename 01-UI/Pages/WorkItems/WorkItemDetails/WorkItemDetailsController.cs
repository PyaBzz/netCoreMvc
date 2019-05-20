using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Providing;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkItems")]
    public class WorkItemDetailsController : BaseController
    {
        private readonly IWorkItemBiz WorkItemBiz;

        public WorkItemDetailsController(IWorkItemBiz workItemBiz)
            => WorkItemBiz = workItemBiz;

        public IActionResult Index(Guid id)
        {
            var viewModel = WorkItemBiz.Get(id);
            return View("WorkItemDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (WorkItemBiz.Delete(id))
            {
                case TransactionResult.NotFound: result = "Found no WorkItem with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkItem with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkItemListController.Index), ShortNameOf<WorkItemListController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
