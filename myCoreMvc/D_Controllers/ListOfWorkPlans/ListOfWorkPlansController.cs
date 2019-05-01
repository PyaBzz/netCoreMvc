using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using PyaFramework.Core;
using myCoreMvc.Services;
using Microsoft.AspNetCore.Authorization;

namespace myCoreMvc.Controllers
{
    public class ListOfWorkPlansController : BaseController
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkPlan>(),
                Message = message
            };
            return View("ListOfWorkPlans", listModel);
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
            return View("ListOfWorkPlans", listModel);
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
