﻿using myCoreMvc.App.Consuming;
using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public class WorkPlanBizOf : IWorkPlanBizOf
    {
        private IDataProvider DataProvider;
        public WorkPlan WorkPlan { get; }

        public WorkPlanBizOf(IDataProvider dataProvider, WorkPlan workPlan)
        {
            DataProvider = dataProvider;
            WorkPlan = workPlan;
        }

        TransactionResult IWorkPlanBizOf.Save() => DataProvider.Save(WorkPlan);
        TransactionResult IWorkPlanBizOf.Delete() => DataProvider.Delete<WorkPlan>(WorkPlan.Id);
    }
}
