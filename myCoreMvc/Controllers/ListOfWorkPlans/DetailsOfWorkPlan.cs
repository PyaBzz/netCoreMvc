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
    }
}
