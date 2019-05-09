using myCoreMvc.App.Consuming;
using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public class WorkPlanBiz : IWorkPlanBiz
    {
        private readonly IDataProvider DataProvider;

        public WorkPlanBiz(IDataProvider dataProvider)
            => DataProvider = dataProvider;

        public WorkPlan Get(Guid id) => DataProvider.Get<WorkPlan>(id);
        public WorkPlan Get(Func<WorkPlan, bool> func) => DataProvider.Get<WorkPlan>(func);

        public List<WorkPlan> GetList() => DataProvider.GetList<WorkPlan>();
        public List<WorkPlan> GetList(Func<WorkPlan, bool> func) => DataProvider.GetList<WorkPlan>(func);
        public List<WorkPlan> GetListIncluding(params Expression<Func<WorkPlan, object>>[] includeProperties)
            => DataProvider.GetListIncluding<WorkPlan>(includeProperties);
        public List<WorkPlan> GetListIncluding(Func<WorkPlan, bool> predicate, params Expression<Func<WorkPlan, object>>[] includeProperties)
            => DataProvider.GetListIncluding<WorkPlan>(predicate, includeProperties);

        public TransactionResult Add(WorkPlan obj) => DataProvider.Add(obj);
        public TransactionResult Update(WorkPlan obj) => DataProvider.Update(obj);
        public TransactionResult Delete(Guid id) => DataProvider.Delete<WorkPlan>(id);
    }
}
