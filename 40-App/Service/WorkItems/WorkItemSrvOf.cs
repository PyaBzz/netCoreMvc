using System;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public class WorkItemSrvOf : IWorkItemSrvOf
    {
        // private readonly IDataRepo DataRepo;
        public WorkItem WorkItem { get; }

        // public WorkItemBizOf(IDataRepo dataRepo, WorkItem workItem)
        // {
        //     DataRepo = dataRepo;
        //     WorkItem = workItem;
        // }

        WorkItem IWorkItemSrvOf.Save() => throw new NotImplementedException();
        void IWorkItemSrvOf.Delete()
        {
            throw new NotImplementedException();
            // DataRepo.Delete<WorkItem>(WorkItem.Id);
        }
    }
}
