using Microsoft.AspNetCore.Mvc;
using PyaFramework.Core;
using Microsoft.AspNetCore.Authorization;

namespace myCoreMvc.UI.Controllers
{
    //Task: Can we use "AuthenticationSchemes = AuthConstants.AuthSchemeName" as a parameter to the Authorize attribute to choose among available schemes?
    [Authorize(Policy = AuthConstants.Level2PolicyName)]
    [Area("Level2Only")]
    public class Level2OnlyController : BaseController
    {
        public ActionResult Index()
        {
            return View("MessageOnly", $">>> Access {AuthConstants.Level2PolicyName} <<<");
        }
    }
}
