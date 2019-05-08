using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using PyaFramework.Core;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanListController : BaseController
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkPlan>(),
                Message = message
            };
            return View("WorkPlanList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = DataProvider.GetList<WorkPlan>();

                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters);
            }
            else
            {
                listModel.Items = DataProvider.GetList<WorkPlan>();
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
