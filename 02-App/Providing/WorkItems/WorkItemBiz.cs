using myCoreMvc.App.Consuming;
using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public class WorkItemBiz : IWorkItemBiz
    {
        private readonly IDataProvider DataProvider;

        public WorkItemBiz(IDataProvider dataProvider)
            => DataProvider = dataProvider;

        public WorkItem Get(Guid id) => DataProvider.Get<WorkItem>(id);
        public WorkItem Get(Func<WorkItem, bool> func) => DataProvider.Get<WorkItem>(func);

        public List<WorkItem> GetList() => DataProvider.GetList<WorkItem>();
        public List<WorkItem> GetList(Func<WorkItem, bool> func) => DataProvider.GetList<WorkItem>(func);
        public List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties)
            => DataProvider.GetListIncluding<WorkItem>(includeProperties);
        public List<WorkItem> GetListIncluding(Func<WorkItem, bool> predicate, params Expression<Func<WorkItem, object>>[] includeProperties)
            => DataProvider.GetListIncluding<WorkItem>(predicate, includeProperties);

        public TransactionResult Add(WorkItem obj) => DataProvider.Add(obj);
        public TransactionResult Update(WorkItem obj) => DataProvider.Update(obj);
        public TransactionResult Delete(Guid id) => DataProvider.Delete<WorkItem>(id);

        public IWorkItemBizOf Of(WorkItem workItem)
            => new WorkItemBizOf(DataProvider, workItem);
    }
}
