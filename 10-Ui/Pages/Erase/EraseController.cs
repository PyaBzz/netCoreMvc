using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App.Services;
using Baz.CoreMvc;
using Baz.Core;

namespace myCoreMvc.UI.Controllers
{
    //Todo: Make available only in dev environment
    [Area("e2eTest")]
    public class EraseController : BaseController
    {
        private readonly IOrderSrv orderSrv;
        private readonly IProductSrv productSrv;
        public EraseController(IOrderSrv o, IProductSrv p)
        {
            orderSrv = o;
            productSrv = p;
        }
        public ActionResult Index()
        {
            orderSrv.DeleteAll();
            productSrv.DeleteAll();
            return View("MessageOnly", "All data erased");
        }
    }
}
