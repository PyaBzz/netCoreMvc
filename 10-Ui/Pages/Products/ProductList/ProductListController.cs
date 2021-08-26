using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using System.Text.RegularExpressions;
using Baz.Core;
using myCoreMvc.App.Services;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.UI.Controllers
{
    [Area("Products")]
    public class ProductListController : BaseController
    {
        IProductRepo ProductRepo;

        public ProductListController(IProductRepo repo)
            => ProductRepo = repo;

        public IActionResult Index(string message)
        {
            var listModel = new ListModel
            {
                Items = ProductRepo.GetAll(),
                Message = message
            };
            return View("ProductList", listModel);
        }

        [HttpPost]
        public IActionResult Index(ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                listModel.Items = ProductRepo.GetAll();
                var searchFilters = new List<Predicate<Product>>();
                if (listModel.Search_Name != null)
                    searchFilters.Add(wi => Regex.IsMatch(wi.Name, listModel.Search_Name));

                listModel.Items = listModel.Items.AppliedWithFilters(searchFilters);
            }
            else
            {
                listModel.Items = ProductRepo.GetAll();
            }
            return View("ProductList", listModel);
        }

        public class ListModel
        {
            public IEnumerable<Product> Items;

            public string Search_Name { get; set; }

            public string Message;
        }
    }
}
