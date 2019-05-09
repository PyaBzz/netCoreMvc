using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using PyaFramework.Core;
using myCoreMvc.App.Providing;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanListController : BaseController
    {
        IWorkPlanBiz WorkPlanBiz;

        public WorkPlanListController(IWorkPlanBiz workPlanBiz)
            => WorkPlanBiz = workPlanBiz;

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = WorkPlanBiz.GetList(),
                Message = message
            };
            return View("WorkPlanList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = WorkPlanBiz.GetList();

                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters);
            }
            else
            {
                listModel.Items = WorkPlanBiz.GetList();
            }
            return View("WorkPlanList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<WorkPlan> Items;
            public List<Func<WorkPlan, bool>> SearchFilters { get; set; } = new List<Func<WorkPlan, bool>>();

            public string Search_Name { get; set; }

            public string Message;
        }
    }
}
