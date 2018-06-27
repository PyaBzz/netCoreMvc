using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Services;
using PooyasFramework;

namespace myCoreMvc
{
    public class BaseController : Controller
    {
        // Every controller inherits from this
        internal IDataProvider DataProvider = DataProviderFactory.DataProvider;
    }
}
