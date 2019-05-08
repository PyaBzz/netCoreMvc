using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PyaFramework.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace myCoreMvc.Controllers
{
    [Area("WorkItems")]
    public class WorkItemListController : BaseController
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetListIncluding<WorkItem>(wi => wi.WorkPlan),
                Message = message
            };
            return View("WorkItemList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = DataProvider.GetList<WorkItem>();

                if (listModel.Search_All != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.GetStringOfProperties(), listModel.Search_All));
                if (listModel.Search_Reference != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Reference, listModel.Search_Reference));
                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));
                if (listModel.Search_Priority != null) listModel.SearchFilters.Add(wi => wi.Priority == listModel.Search_Priority);

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters); //Task: Should we filter in back-end instead?
            }
            else
            {
                listModel.Items = DataProvider.GetList<WorkItem>();
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
