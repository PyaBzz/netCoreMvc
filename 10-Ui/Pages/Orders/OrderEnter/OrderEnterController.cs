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
        private readonly IOrderSrv orderSrv;
        private readonly IProductSrv productSrv;

        public OrderEnterController(IOrderSrv o, IProductSrv p)
        {
            orderSrv = o;
            productSrv = p;
        }

        public IActionResult Index(Guid? id)
        {
            var inputModel = new EnterModel();
            inputModel.PriorityChoices = Order.PriorityChoices.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString(), Selected = x == inputModel.Priority });
            inputModel.ProductChoices = productSrv.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == inputModel.ProductId });
            var item = id.HasValue ? orderSrv.Get(id) : new Order();
            inputModel.CopySimilarPropertiesFrom(item);
            inputModel.ProductId = item.ProductId;
            return View("OrderEnter", inputModel);
        }

        [HttpPost]
        //[CustomExceptionFilter]
        public IActionResult Index(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                inputModel.PriorityChoices = Order.PriorityChoices.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString(), Selected = x == inputModel.Priority });
                inputModel.ProductChoices = productSrv.GetAll().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = x.Id == inputModel.ProductId });
                var order = new Order();
                order.CopySimilarPropertiesFrom(inputModel);  // Prevents malicious over-posting
                orderSrv.Save(order);
                string resultMessage = "Item saved";
                return RedirectToAction(nameof(OrderListController.Index), Short<OrderListController>.Name, new { message = resultMessage });
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
            public Guid? Id { get; set; }
            [Display(Name = "Item name")]
            [ValidateAlphanumeric(3, 16)]
            public string Name { get; set; }
            public Guid? ProductId { get; set; }
            public string Reference { get; set; }
            public int Priority { get; set; }
            public IEnumerable<SelectListItem> PriorityChoices { get; set; }
            public IEnumerable<SelectListItem> ProductChoices { get; set; }
            public string Message = "";
        }
    }
}