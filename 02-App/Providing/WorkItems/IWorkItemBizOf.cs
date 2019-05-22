using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public interface IWorkItemBizOf
    {
        WorkItem WorkItem { get; }

        TransactionResult Add();
        TransactionResult Update();
        TransactionResult Delete();
    }
}
