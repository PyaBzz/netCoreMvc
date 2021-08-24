using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class WorkItemSrv : IWorkItemSrv
    {
        // private readonly IDataRepo DataRepo;

        // public WorkItemBiz(IDataRepo dataRepo)
        //     => DataRepo = dataRepo;

        public WorkItem Get(Guid id) => throw new NotImplementedException();
        public WorkItem Get(Predicate<WorkItem> func) => throw new NotImplementedException();

        public List<WorkItem> GetList() => throw new NotImplementedException();
        public List<WorkItem> GetList(Predicate<WorkItem> func) => throw new NotImplementedException();
        public List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties)
            => throw new NotImplementedException();
        public List<WorkItem> GetListIncluding(Predicate<WorkItem> predicate, params Expression<Func<WorkItem, object>>[] includeProperties)
            => throw new NotImplementedException();

        public IWorkItemSrvOf Of(WorkItem workItem)
            => throw new NotImplementedException();
    }
}
