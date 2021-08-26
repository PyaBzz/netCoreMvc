using System;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Baz.Core;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Baz.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("Products")]
    public class ProductEnterController : BaseController
    {
        private readonly IProductRepo ProductRepo;

        public ProductEnterController(IProductRepo repo)
            => ProductRepo = repo;

        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel();
            if (id != Guid.Empty)
            {
                var product = ProductRepo.Get(id);
                if (product != null) inputModel.CopySimilarPropertiesFrom(product);
            }
            return View("ProductEnter", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            throw new NotImplementedException();
            // if (ModelState.IsValid)
            // {
            //     var product = inputModel.Id == Guid.Empty
            //         ? new Product()
            //         : ProductRepo.Get(inputModel.Id);  //Task: Instead of finding the object again, cache it in the view model as inputModel.Item

            //     product.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
            //     var transactionResult = ProductRepo.Add(product);

            //     string resultMessage;
            //     switch (transactionResult)
            //     {
            //         case TransactionResult.Updated: resultMessage = "Item updated"; break;
            //         case TransactionResult.Added: resultMessage = "New item added"; break;
            //         default: resultMessage = transactionResult.ToString(); break;
            //     }

            //     return RedirectToAction(nameof(ProductListController.Index), Short<ProductListController>.Name, new { message = resultMessage });  // Prevents re-submission by refresh
            // }
            // else
            // {
            //     inputModel.Message = "Invalid values for: "
            //         + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
            //     return View("ProductEnter", inputModel);
            // }
        }

        public class EnterModel : IClonable
        {
            public Guid Id { get; set; }

            [Display(Name = "Plan name")]
            [Required(ErrorMessage = "{0} is mandatory.")]
            [StringLength(16, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters in length.")]
            [RegularExpression("^[A-Z][a-zA-Z0-9]*", ErrorMessage = "{0} must start with a capital letter and may only contain alphanumeric characters.")]
            public string Name { get; set; }

            public string Message = "";
        }
    }
}
