using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public interface IWorkPlanBizOf
    {
        WorkPlan WorkPlan { get; }

        TransactionResult Save();
        TransactionResult Delete();
    }
}
