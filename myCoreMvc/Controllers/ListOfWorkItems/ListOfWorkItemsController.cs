using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace myCoreMvc
{
    public class ListOfWorkItemsController : Controller
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkItem>(),
                Message = message
            };
            return View("ListOfWorkItems", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = DataProvider.GetList<WorkItem>(wi => wi.Name == listModel.Search_Name);  // TODO: Further develop the search feature based on RegEx.
                listModel.Message = listModel.Search_Name;

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
            public string Message;
            public string Search_Name { get; set; }
        }
    }
}
