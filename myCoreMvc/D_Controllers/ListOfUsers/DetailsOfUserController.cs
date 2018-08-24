using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using PooyasFramework;
using System;

namespace myCoreMvc.Controllers
{
    public class DetailsOfUserController : BaseController
    { //Task: Use tables like the CookieEditor for all details pages
        public IActionResult Index(Guid id)
        {
            var viewModel = DataProvider.Get<User>(wi => wi.Id == id);
            return View("~/Views/ListOfUsers/DetailsOfUser.cshtml", viewModel);
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
            return RedirectToAction(nameof(ListOfUsersController.Index), ShortNameOf<ListOfUsersController>(), new { message = result });  // Prevents re-submission by refresh
        }
    }
}
