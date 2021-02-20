using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Services
{
    public interface IWorkItemBizOf
    {
        WorkItem WorkItem { get; }

        TransactionResult Save();
        TransactionResult Delete();
    }
}
