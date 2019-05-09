﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using PyaFramework.Core;
using myCoreMvc.App.Providing;

namespace myCoreMvc.UI.Controllers
{
    [Area("WorkItems")]
    public class WorkItemListController : BaseController
    {
        private readonly IWorkItemBiz WorkItemBiz;

        public WorkItemListController(IWorkItemBiz workItemBiz)
            => WorkItemBiz = workItemBiz;

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = WorkItemBiz.GetListIncluding(wi => wi.WorkPlan),
                Message = message
            };
            return View("WorkItemList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = WorkItemBiz.GetList();

                if (listModel.Search_All != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.GetStringOfProperties(), listModel.Search_All));
                if (listModel.Search_Reference != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Reference, listModel.Search_Reference));
                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));
                if (listModel.Search_Priority != null) listModel.SearchFilters.Add(wi => wi.Priority == listModel.Search_Priority);

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters); //Task: Should we filter in back-end instead?
            }
            else
            {
                listModel.Items = WorkItemBiz.GetList();
            }
            return View("WorkItemList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<WorkItem> Items;
            public List<Func<WorkItem, bool>> SearchFilters { get; set; } = new List<Func<WorkItem, bool>>();

            public string Search_All { get; set; }
            public string Search_Reference { get; set; }
            public string Search_Name { get; set; }
            public int? Search_Priority { get; set; }

            public string Message;
        }
    }
}