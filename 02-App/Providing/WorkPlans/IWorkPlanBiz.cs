using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    //Task: Apply IWorkPlanBizOf pattern
    public interface IWorkPlanBiz
    {
        WorkPlan Get(Guid id);
        WorkPlan Get(Func<WorkPlan, bool> func);

        List<WorkPlan> GetList();
        List<WorkPlan> GetList(Func<WorkPlan, bool> func);
        List<WorkPlan> GetListIncluding(params Expression<Func<WorkPlan, object>>[] includeProperties);
        List<WorkPlan> GetListIncluding(Func<WorkPlan, bool> predicate, params Expression<Func<WorkPlan, object>>[] includeProperties);

        TransactionResult Add(WorkPlan obj);
        TransactionResult Update(WorkPlan obj);
        TransactionResult Delete(Guid id);
    }
}
