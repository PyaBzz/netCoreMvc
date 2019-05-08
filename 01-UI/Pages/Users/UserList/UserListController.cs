using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using PyaFramework.Core;

namespace myCoreMvc.UI.Controllers
{
    [Area("Users")]
    public class UserListController : BaseController
    {
        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<User>(),
                Message = message
            };
            return View("UserList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = DataProvider.GetList<User>();

                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters);
            }
            else
            {
                listModel.Items = DataProvider.GetList<User>();
            }
            return View("UserList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<User> Items;
            public List<Func<User, bool>> SearchFilters { get; set; } = new List<Func<User, bool>>();

            public string Search_Name { get; set; }

            public string Message;
        }
    }
}
