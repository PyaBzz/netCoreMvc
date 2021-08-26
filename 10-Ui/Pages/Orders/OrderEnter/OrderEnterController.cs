using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Baz.Core;
using Baz.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using myCoreMvc.App.Services;
using Baz.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Area("Orders")]
    public class OrderEnterController : BaseController
    {
        private readonly IOrderSrv OrderBiz;
        private readonly IProductRepo ProductRepo;

        public OrderEnterController(IOrderSrv orderBiz, IProductRepo productBiz)
        {
            OrderBiz = orderBiz;
            ProductRepo = productBiz;
        }

        public IActionResult Index(Guid id)
        {
            var inputModel = new EnterModel();
            inputModel.PriorityChoices = Order.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == inputModel.Priority });
            inputModel.ProductChoices = ProductRepo.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == inputModel.Product });
            if (id != Guid.Empty)
            {
                var item = OrderBiz.Get(id);
                if (item != null)
                {
                    inputModel.CopySimilarPropertiesFrom(item);
                    inputModel.Product = item.Product.Id.Value; //Task: Product itself works in Get but its Guid works for POST. Find a way to cover both.
                }
            }
            return View("OrderEnter", inputModel);
        }

        [HttpPost]
        //[CustomExceptionFilter]
        public IActionResult Index(EnterModel inputModel)
        {
            throw new NotImplementedException();
            if (ModelState.IsValid)
            {
                #region Lesson
                // We could work with ModelState errors in details using the following:
                // ModelState.AddModelError("Reference", "It must be in blabla format!")
                // ModelState.AddModelError("", "This is an object level error rather than property level.")
                // @Html.ValidationSummary(true)
                // @Html.ValidationMessageFor(p => p.Reference)
                #endregion

                // inputModel.PriorityChoices = Order.PriorityChoices.Select(c => new SelectListItem { Text = c.ToString(), Value = c.ToString(), Selected = c == inputModel.Priority });
                // inputModel.ProductChoices = ProductRepo.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = c.Id == inputModel.Product });

                // var order = inputModel.Id == Guid.Empty
                //     ? new Order()
                //     : OrderBiz.Get(inputModel.Id);  //Task: Instead of finding the object again, cache it in the view model as inputModel.Item

                // order.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                // workIteProductan ProductanRepo.Get(inputModeProductan); //Task: This is because the getter method from DataRepo is shallow. Could we make it deep to returProductan as well?
                // var transactionResult = OrderBiz.Of(order).Save();

                // string resultMessage;
                // switch (transactionResult)
                // {
                //     case TransactionResult.Updated: resultMessage = "Item updated"; break;
                //     case TransactionResult.Added: resultMessage = "New item added"; break;
                //     default: resultMessage = transactionResult.ToString(); break;
                // }

                // return RedirectToAction(nameof(OrderListController.Index), Short<OrderListController>.Name, new { message = resultMessage });  // Prevents re-submission by refresh
            }
            else
            {
                inputModel.Message = "Invalid values for: "
                    + ModelState.Where(p => p.Value.ValidationState == ModelValidationState.Invalid).Select(p => p.Key).ToString(", ");
                return View("OrderEnter", inputModel);
            }
        }

        public class EnterModel : IClonable
        {
            #region Lesson
            //View models should be simpe data containers.
            //They should contain no behavior and neither expose nor hold any dependencies.
            //This forces the Razor views to be simple and stupid and hence most maintainable.
            //When you follow that practice, only the controller will (and should) have dependencies.
            //Then the controller should set necessary properties on the VM
            //This may require filtering or policy enforcement based on business requirements none of which is a concern of VM!
            #endregion

            public Guid Id { get; set; }
            [Display(Name = "Item name")]
            [ValidateAlphanumeric(3, 16)]
            public string Name { get; set; }
            public Guid Product { get; set; }
            public string Reference { get; set; }
            public int Priority { get; set; }
            public IEnumerable<SelectListItem> PriorityChoices { get; set; }
            public IEnumerable<SelectListItem> ProductChoices { get; set; }
            public string Message = "";
        }
    }
}