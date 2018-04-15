using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myCoreMvc.Models;
using System.ComponentModel.DataAnnotations;

namespace myCoreMvc
{
    public class ListOfWorkItemsController : Controller
    {
        public IActionResult Index(string result)
        {
            var listModel = new ListModel
            {
                Items = DataProvider.GetList<WorkItem>(),
                Message = result
            };
            return View("ListOfWorkItems", listModel);
        }

        public IActionResult Details(Guid id)
        {
            var viewModel = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            return View("ListOfWorkItemsDetails", viewModel);
        }

        public IActionResult Enter(Guid id)
        {
            var item = DataProvider.Get<WorkItem>(wi => wi.Id == id);
            var inputModel = new EnterModel();
            if (item != null)
            {
                inputModel.Id = item.Id;
                inputModel.Reference = item.Reference;
                inputModel.Name = item.Name;
                inputModel.Priority = item.Priority;
            }
            inputModel.PriorityChoices = WorkItem.PriorityChoices;
            return View("ListOfWorkItemsEnter", inputModel);  // TODO: Use "asp-" tag helpers instead of tags attributes.
                                                              // TODO: See if you can minimise duplicate markup in the view.
        }

        [HttpPost]
        public IActionResult Enter(EnterModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var listModel = new ListModel();
                var workItem = new WorkItem  // We use this simple way to prevent malicious over-posting
                {
                    Id = inputModel.Id,
                    Reference = inputModel.Reference,
                    Name = inputModel.Name,
                    Priority = inputModel.Priority
                };
                var result = "";
                switch (DataProvider.Save(workItem))
                {
                    case DataProvider.TransactionResult.Updated: result = "Item updated"; break;
                    case DataProvider.TransactionResult.Added: result = "New item added"; break;
                    default: result = "New item added"; break;
                }
                return RedirectToAction("Index", new { result });  // Prevents form re-submission by refresh
            }
            else
            {
                var correctionInputModel = new EnterModel  // TODO: Replace with a cloning method implemented at parent level for all object types
                {
                    Id = inputModel.Id,
                    Reference = inputModel.Reference,
                    Name = inputModel.Name,
                    Priority = inputModel.Priority,
                    PriorityChoices = WorkItem.PriorityChoices
                };
                return View("ListOfWorkItemsEnter", correctionInputModel);
            }
        }

        public class ListModel
        {
            public IEnumerable<WorkItem> Items;
            public string Message;
        }

        public class EnterModel
        {
            public Guid Id { get; set; }
            public String Reference { get; set; }
            public int Priority { get; set; }
            [Display(Name = "Item name"), Required]
            public string Name { get; set; }

            public IEnumerable<int> PriorityChoices;
        }
    }
}
