using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class WorkPlanBiz : IWorkPlanBiz
    {
        private readonly IDataRepo DataRepo;

        public WorkPlanBiz(IDataRepo dataRepo)
            => DataRepo = dataRepo;

        public WorkPlan Get(Guid id) => DataRepo.Get<WorkPlan>(id);
        public WorkPlan Get(Func<WorkPlan, bool> func) => DataRepo.Get<WorkPlan>(func);

        public List<WorkPlan> GetList() => DataRepo.GetList<WorkPlan>();
        public List<WorkPlan> GetList(Func<WorkPlan, bool> func) => DataRepo.GetList<WorkPlan>(func);
        public List<WorkPlan> GetListIncluding(params Expression<Func<WorkPlan, object>>[] includeProperties)
            => DataRepo.GetListIncluding<WorkPlan>(includeProperties);
        public List<WorkPlan> GetListIncluding(Func<WorkPlan, bool> predicate, params Expression<Func<WorkPlan, object>>[] includeProperties)
            => DataRepo.GetListIncluding<WorkPlan>(predicate, includeProperties);

        IWorkPlanBizOf IWorkPlanBiz.Of(WorkPlan workPlan)
            => new WorkPlanBizOf(DataRepo, workPlan);
    }
}
