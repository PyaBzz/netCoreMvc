using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Baz.CoreMvc;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("Products")]
    public class ProductDetailsController : BaseController
    {
        private readonly IProductRepo ProductRepo;

        public ProductDetailsController(IProductRepo repo)
            => ProductRepo = repo;

        public IActionResult Index(Guid id)
        {
            var viewModel = ProductRepo.Get(id);
            return View("ProductDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
            // var result = "";
            // switch (ProductRepo.Delete(id))
            // {
            //     case TransactionResult.NotFound: result = "Found no Product with the provided Id."; break;
            //     case TransactionResult.Deleted: result = "Item deleted."; break;
            //     default: result = "Found no Product with the provided Id."; break;
            // }
            // return RedirectToAction(nameof(ProductListController.Index), Short<ProductListController>.Name, new { message = result });  // Prevents re-submission by refresh
        }
    }
}
