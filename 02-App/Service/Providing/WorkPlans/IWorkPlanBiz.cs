using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public interface IWorkPlanBiz
    {
        WorkPlan Get(Guid id);
        WorkPlan Get(Func<WorkPlan, bool> func);

        List<WorkPlan> GetList();
        List<WorkPlan> GetList(Func<WorkPlan, bool> func);
        List<WorkPlan> GetListIncluding(params Expression<Func<WorkPlan, object>>[] includeProperties);
        List<WorkPlan> GetListIncluding(Func<WorkPlan, bool> predicate, params Expression<Func<WorkPlan, object>>[] includeProperties);

        IWorkPlanBizOf Of(WorkPlan workPlan);
    }
}
