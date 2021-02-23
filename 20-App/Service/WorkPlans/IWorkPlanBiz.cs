using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public interface IWorkPlanBiz
    {
        WorkPlan Get(Guid id);
        WorkPlan Get(Predicate<WorkPlan> func);

        List<WorkPlan> GetList();
        List<WorkPlan> GetList(Predicate<WorkPlan> func);
        List<WorkPlan> GetListIncluding(params Expression<Func<WorkPlan, object>>[] includeProperties);
        List<WorkPlan> GetListIncluding(Predicate<WorkPlan> predicate, params Expression<Func<WorkPlan, object>>[] includeProperties);

        IWorkPlanBizOf Of(WorkPlan workPlan);
    }
}
