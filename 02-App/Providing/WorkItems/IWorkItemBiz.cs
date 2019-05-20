using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    //Task: Apply IWorkItemBizOf pattern
    public interface IWorkItemBiz
    {
        WorkItem Get(Guid id);
        WorkItem Get(Func<WorkItem, bool> func);

        List<WorkItem> GetList();
        List<WorkItem> GetList(Func<WorkItem, bool> func);
        List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties);
        List<WorkItem> GetListIncluding(Func<WorkItem, bool> predicate, params Expression<Func<WorkItem, object>>[] includeProperties);

        TransactionResult Add(WorkItem obj);
        TransactionResult Update(WorkItem obj);
        TransactionResult Delete(Guid id);
    }
}
