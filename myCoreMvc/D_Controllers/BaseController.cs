using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using PooyasFramework;

namespace myCoreMvc.Controllers
{
    public class BaseController : Controller
    {
        internal IDataProvider DataProvider = ServiceInjector.Resolve<IDataProvider>();

        internal static string ShortNameOf<T>() where T : Controller
        {
            return typeof(T).Name.TrimEnd("Controller");
        }
    }
}
