using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App.Services;

namespace myCoreMvc.UI.Controllers
{
    [Area("Orders")]
    public class OrderListController : BaseController
    {
        private readonly IOrderSrv orderSrv;

        public OrderListController(IOrderSrv o) => orderSrv = o;

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = orderSrv.GetAll(),
                Message = message
            };
            return View("OrderList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = orderSrv.GetAll();
                var searchFilters = new List<Predicate<Order>>();
                if (listModel.Search_All != null) searchFilters.Add(wi => Regex.IsMatch(wi.GetStringOfProperties(), listModel.Search_All));
                if (listModel.Search_Reference != null) searchFilters.Add(wi => Regex.IsMatch(wi.Reference, listModel.Search_Reference));
                if (listModel.Search_Name != null) searchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));
                if (listModel.Search_Priority != null) searchFilters.Add(wi => wi.Priority == listModel.Search_Priority);

                listModel.Items = listModel.Items.AppliedWithFilters(searchFilters);
            }
            else
            {
                listModel.Items = orderSrv.GetAll();
            }
            return View("OrderList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<Order> Items;

            public string Search_All { get; set; }
            public string Search_Reference { get; set; }
            public string Search_Name { get; set; }
            public int? Search_Priority { get; set; }

            public string Message;
        }
    }
}
