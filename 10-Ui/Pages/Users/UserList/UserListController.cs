using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App.Services;

namespace myCoreMvc.UI.Controllers
{
    [Area("Users")]
    public class UserListController : BaseController
    {
        private readonly IUserBiz UserBiz;

        public UserListController(IUserBiz userBiz)
            => UserBiz = userBiz;

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
                var searchFilters = new List<Predicate<User>>();
                if (listModel.Search_Name.HasValue())
                    searchFilters.Add((User u) => Regex.IsMatch(u.Name, listModel.Search_Name));
                listModel.Items = listModel.Items.AppliedWithFilters(searchFilters);
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
            public string Search_Name { get; set; }
            public string Message;
        }
    }
}
