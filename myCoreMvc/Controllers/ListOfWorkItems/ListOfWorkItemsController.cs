using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PooyasFramework;

namespace myCoreMvc
{
    public class ListOfWorkItemsController : Controller
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkItem>(),
                Message = message //TODO: Find a way to render footer from its own controller and show IP address of the client in it.
            };
            return View("ListOfWorkItems", listModel);
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

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters);
            }
            else
            {
                listModel.Items = DataProvider.GetList<WorkItem>();
            }
            return View("ListOfWorkItems", listModel);
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
