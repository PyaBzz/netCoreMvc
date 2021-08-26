using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("Products")]
    public class ProductDetailsController : BaseController
    {
        private readonly IProductSrv srv;

        public ProductDetailsController(IProductSrv r)
            => srv = r;

        public IActionResult Index(Guid id)
        {
            var viewModel = srv.Get(id);
            return View("ProductDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            srv.Delete(id);
            var result = "Item deleted";
            return RedirectToAction(nameof(ProductListController.Index), Short<ProductListController>.Name, new { message = result });
        }
    }
}
