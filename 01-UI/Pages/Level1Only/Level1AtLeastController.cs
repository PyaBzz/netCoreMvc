using Microsoft.AspNetCore.Mvc;
using Py.Core;
using Microsoft.AspNetCore.Authorization;
using Py.CoreMvc;

namespace myCoreMvc.UI.Controllers
{
    [Authorize]
    [Area("Level1Only")]
    public class Level1AtLeastController : BaseController
    {
        public ActionResult Index()
        {
            return View("MessageOnly", $"Access level 1 granted");
        }
    }
}
