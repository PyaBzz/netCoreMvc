using System;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using myCoreMvc.Models;
using PooyasFramework;
using PooyasFramework.IoC;

namespace myCoreMvc.Controllers
{
    public class ExperimentsController : BaseController
    {
        //TODO: Could we use reflection to automatically add all action methods in this controller to the top menu?
        //We could perhaps use something similar to: registeredObject.ConcreteType.GetConstructors().First()
        [Route("db")]
        public ActionResult Database()
        {
            var instance = Db.Get<WorkItem>(5);
            return View("~/Views/ListOfWorkItems/DetailsOfWorkItem.cshtml", instance);
        }

        [Route("delegate")]
        public ContentResult Delegate()
        {
            var result = "";

            DelegateType hasang;

            hasang = Delegates.DelegateImplementation1;
            result += hasang(8) + Environment.NewLine;

            hasang -= Delegates.DelegateImplementation1;
            hasang += Delegates.DelegateImplementation2;
            result += hasang(8) + Environment.NewLine;

            return Content(result);
        }

        [Route("ioc")]
        public ContentResult Ioc()
        {
            var iocContainer = new IocContainer();
            iocContainer.Register<IClonable, WorkItem>(Injection.Singleton);
            var resolvedObj = iocContainer.Resolve<IClonable>();
            //var result = resolvedObj.GetStringOfAllProperties();
            var result = resolvedObj.ToString();
            return Content(result);
        }
    }
}
