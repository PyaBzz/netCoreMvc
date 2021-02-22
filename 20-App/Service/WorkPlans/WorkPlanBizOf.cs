using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public class WorkPlanBizOf : IWorkPlanBizOf
    {
        private IDataRepo DataRepo;
        public WorkPlan WorkPlan { get; }

        public WorkPlanBizOf(IDataRepo dataRepo, WorkPlan workPlan)
        {
            DataRepo = dataRepo;
            WorkPlan = workPlan;
        }

        TransactionResult IWorkPlanBizOf.Save() => DataRepo.Save(WorkPlan);
        TransactionResult IWorkPlanBizOf.Delete() => DataRepo.Delete<WorkPlan>(WorkPlan.Id);
    }
}
