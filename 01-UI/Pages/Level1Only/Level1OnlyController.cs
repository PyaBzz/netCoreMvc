using Microsoft.AspNetCore.Mvc;
using PyaFramework.Core;
using Microsoft.AspNetCore.Authorization;

namespace myCoreMvc.UI.Controllers
{
    [Authorize]
    [Area("Level1Only")]
    public class Level1OnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("MessageOnly", $">>> Access {AuthConstants.Level1PolicyName} (Only Authenticated) <<<");
        }
    }
}
