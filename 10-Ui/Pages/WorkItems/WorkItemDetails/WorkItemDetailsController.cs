using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using System;

namespace myCoreMvc.UI.Controllers
{
    [Area("Orders")]
    public class OrderDetailsController : BaseController
    {
        private readonly IOrderBiz OrderBiz;

        public OrderDetailsController(IOrderBiz orderBiz)
            => OrderBiz = orderBiz;

        public IActionResult Index(Guid id)
        {
            var viewModel = OrderBiz.Get(id);
            return View("OrderDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
            // var result = "";
            // var order = OrderBiz.Get(id);
            // switch (OrderBiz.Of(order).Delete())
            // {
            //     case TransactionResult.NotFound: result = "Found no Order with the provided Id."; break;
            //     case TransactionResult.Deleted: result = "Item deleted."; break;
            //     default: result = "Found no Order with the provided Id."; break;
            // }
            // return RedirectToAction(nameof(OrderListController.Index), Short<OrderListController>.Name, new { message = result });  // Prevents re-submission by refresh
        }
    }
}
