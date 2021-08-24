using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public interface IWorkItemSrv
    {
        WorkItem Get(Guid id);
        WorkItem Get(Predicate<WorkItem> func);

        List<WorkItem> GetList();
        List<WorkItem> GetList(Predicate<WorkItem> func);
        List<WorkItem> GetListIncluding(params Expression<Func<WorkItem, object>>[] includeProperties);
        List<WorkItem> GetListIncluding(Predicate<WorkItem> predicate, params Expression<Func<WorkItem, object>>[] includeProperties);

        IWorkItemSrvOf Of(WorkItem workItem);
    }
}
