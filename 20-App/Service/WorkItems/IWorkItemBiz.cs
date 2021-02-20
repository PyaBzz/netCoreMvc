using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public interface IWorkItemBiz
    {
        WorkItem Get(Guid id);
        WorkItem Get(Func<WorkItem, bool> func);

        List<WorkItem> GetList();
        List<WorkItem> GetList(Func<WorkItem, bool> func);
        List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties);
        List<WorkItem> GetListIncluding(Func<WorkItem, bool> predicate, params Expression<Func<WorkItem, object>>[] includeProperties);

        IWorkItemBizOf Of(WorkItem workItem);
    }
}
