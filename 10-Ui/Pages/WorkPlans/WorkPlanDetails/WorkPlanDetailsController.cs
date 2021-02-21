﻿using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanDetailsController : BaseController
    {
        private readonly IWorkPlanBiz WorkPlanBiz;

        public WorkPlanDetailsController(IWorkPlanBiz workPlanBiz)
            => WorkPlanBiz = workPlanBiz;

        public IActionResult Index(Guid id)
        {
            var viewModel = WorkPlanBiz.Get(id);
            return View("WorkPlanDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            var workPlan = WorkPlanBiz.Get(id);
            switch (WorkPlanBiz.Of(workPlan).Delete())
            {
                case TransactionResult.NotFound: result = "Found no WorkPlan with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no WorkPlan with the provided Id."; break;
            }
            return RedirectToAction(nameof(WorkPlanListController.Index), Short<WorkPlanListController>.Name, new { message = result });  // Prevents re-submission by refresh
        }
    }
}