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
        public IActionResult Index(string result)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkItem>(),
                Message = result
            };
            return View("ListOfWorkItems", listModel);
        }

        public class ListModel
        {
            public IEnumerable<WorkItem> Items;
            public string Message;
        }
    }
}
