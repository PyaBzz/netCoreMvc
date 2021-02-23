using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class WorkItemBiz : IWorkItemBiz
    {
        private readonly IDataRepo DataRepo;

        public WorkItemBiz(IDataRepo dataRepo)
            => DataRepo = dataRepo;

        public WorkItem Get(Guid id) => DataRepo.Get<WorkItem>(id);
        public WorkItem Get(Predicate<WorkItem> func) => DataRepo.Get<WorkItem>(func);

        public List<WorkItem> GetList() => DataRepo.GetList<WorkItem>();
        public List<WorkItem> GetList(Predicate<WorkItem> func) => DataRepo.GetList<WorkItem>(func);
        public List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties)
            => DataRepo.GetListIncluding<WorkItem>(includeProperties);
        public List<WorkItem> GetListIncluding(Predicate<WorkItem> predicate, params Expression<Func<WorkItem, object>>[] includeProperties)
            => DataRepo.GetListIncluding<WorkItem>(predicate, includeProperties);

        public IWorkItemBizOf Of(WorkItem workItem)
            => new WorkItemBizOf(DataRepo, workItem);
    }
}
