using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using PyaFramework.Core;

namespace myCoreMvc.Controllers
{
    public class BaseController : Controller
    {
        internal IDataProvider DataProvider => HttpContext.RequestServices.GetService(typeof(IDataProvider)) as IDataProvider;

        internal static string ShortNameOf<T>() where T : Controller
        {
            return typeof(T).Name.TrimEnd("Controller");
        }
    }
}
