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

        public IActionResult Index(Guid? id)
        {
            var inputModel = new EnterModel();
            if (id != null)
            {
                var product = ProductRepo.Get(id);
                if (product != null) inputModel.CopySimilarPropertiesFrom(product); //Todo: Should throw a not-found exception
            }
            return View("ProductEnter", inputModel);
        }

        [HttpPost]
        public IActionResult Index(EnterModel inputModel)
        {
            // throw new NotImplementedException();
            if (ModelState.IsValid)
            {
                var product = new Product();  //Task: Instead of finding the object again, cache it in the view model as inputModel.Item

                product.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                var resultId = ProductRepo.Save(product);

                var resultMessage = "Item saved";
                return RedirectToAction(nameof(ProductListController.Index), Short<ProductListController>.Name, new { message = resultMessage });
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("ProductEnter", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            public Guid? Id { get; set; }

            [Display(Name = "Product name")]
            [Required(ErrorMessage = "{0} is mandatory.")]
            [StringLength(16, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters in length.")]
            [RegularExpression("^[A-Z][a-zA-Z0-9]*", ErrorMessage = "{0} must start with a capital letter and may only contain alphanumeric characters.")]
            public string Name { get; set; }

            public string Message = "";
        }
    }
}
