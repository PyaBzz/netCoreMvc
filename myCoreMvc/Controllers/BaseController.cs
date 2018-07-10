using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using PooyasFramework;

namespace myCoreMvc.Controllers
{
    public class BaseController : Controller
    {
        //TODO: Read this carefully:
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/dependency-injection?view=aspnetcore-2.1
        internal IDataProvider DataProvider = ServiceInjector.Resolve<IDataProvider>();
    }
}
