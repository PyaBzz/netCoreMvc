using Microsoft.AspNetCore.Mvc;
using myCoreMvc.App;
using PyaFramework.Core;

namespace myCoreMvc.UI.Controllers
{
    public class BaseController : Controller
    {
        internal static string ShortNameOf<T>() where T : Controller
            => typeof(T).Name.TrimEnd("Controller");
    }
}
