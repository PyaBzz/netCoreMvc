using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App.Services;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkPlans")]
    public class WorkPlanListController : BaseController
    {
        IWorkplanRepo WorkPlanRepo;

        public WorkPlanListController(IWorkplanRepo repo)
            => WorkPlanRepo = repo;

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = WorkPlanRepo.GetAll(),
                Message = message
            };
            return View("WorkPlanList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = WorkPlanRepo.GetAll();
                var searchFilters = new List<Predicate<WorkPlan>>();
                if (listModel.Search_Name != null)
                    searchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(searchFilters);
            }
            else
            {
                listModel.Items = WorkPlanRepo.GetAll();
            }
            return View("WorkPlanList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<WorkPlan> Items;

            public string Search_Name { get; set; }

            public string Message;
        }
    }
}
