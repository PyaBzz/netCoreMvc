﻿using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
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
            throw new NotImplementedException();
            // var result = "";
            // var workItem = WorkItemBiz.Get(id);
            // switch (WorkItemBiz.Of(workItem).Delete())
            // {
            //     case TransactionResult.NotFound: result = "Found no WorkItem with the provided Id."; break;
            //     case TransactionResult.Deleted: result = "Item deleted."; break;
            //     default: result = "Found no WorkItem with the provided Id."; break;
            // }
            // return RedirectToAction(nameof(WorkItemListController.Index), Short<WorkItemListController>.Name, new { message = result });  // Prevents re-submission by refresh
        }
    }
}
