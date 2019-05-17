using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using PyaFramework.Core;
using myCoreMvc.App.Providing;

namespace myCoreMvc.UI.Controllers
{
    [Area("Users")]
    public class UserListController : BaseController
    {
        private readonly IUserBiz UserBiz;

        public UserListController(IUserBiz userBiz)
        {
            UserBiz = userBiz;
        }

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = UserBiz.GetList(),
                Message = message
            };
            return View("UserList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = UserBiz.GetList();

                if (listModel.Search_Name != null) listModel.SearchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(listModel.SearchFilters);
            }
            else
            {
                listModel.Items = UserBiz.GetList();
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
