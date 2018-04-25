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
            listModel.Message = listModel.Items.First().ToString();
            return View("ListOfWorkItems", listModel);
        }

        // TODO: Implement search using [https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-2.1]
        //and [https://stackoverflow.com/questions/41577376/how-to-read-values-from-the-querystring-with-asp-net-core?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa]

        public class ListModel
        {
            public IEnumerable<WorkItem> Items;
            public string Message;
        }
    }
}
