using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc
{
    public class DetailsOfWorkItem : Controller
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", viewModel);
        }
    }
}
