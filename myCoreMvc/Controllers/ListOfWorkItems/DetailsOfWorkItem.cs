using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc
{
    public class DetailsOfWorkItem : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (DataProvider.Delete<WorkItem>(id))
            {
                case DataProvider.TransactionResult.NotFound: result = "Found no WorkItem with the provided Id."; break;
                case DataProvider.TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkItem with the provided Id."; break;
            }
            return RedirectToAction("Index", "ListOfWorkItems", new { message = result });  // Prevents form re-submission by refresh
        }
    }
}
