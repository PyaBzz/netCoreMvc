using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Domain;
using PyaFramework.Core;
using System;

namespace myCoreMvc.Controllers
{
    [Area("Users")]
    public class UserDetailsController : BaseController
    {
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<User>(id);
            return View("UserDetails", viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var result = "";
            switch (DataProvider.Delete<User>(id))
            {
                case TransactionResult.NotFound: result = "Found no User with the provided Id."; break;
                case TransactionResult.Deleted: result = "Item deleted."; break;
                default: result = "Found no User with the provided Id."; break;
            }
            return RedirectToAction(nameof(UserListController.Index), ShortNameOf<UserListController>(), new { area = "Users", message = result });  // Prevents re-submission by refresh
        }
    }
}
