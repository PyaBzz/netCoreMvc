using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class WorkItemBizOf : IWorkItemBizOf
    {
        private readonly IDataRepo DataRepo;
        public WorkItem WorkItem { get; }

        public WorkItemBizOf(IDataRepo dataRepo, WorkItem workItem)
        {
            DataRepo = dataRepo;
            WorkItem = workItem;
        }

        TransactionResult IWorkItemBizOf.Save() => DataRepo.Save(WorkItem);
        TransactionResult IWorkItemBizOf.Delete() => DataRepo.Delete<WorkItem>(WorkItem.Id);
    }
}
