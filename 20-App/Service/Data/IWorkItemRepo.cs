using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IWorkItemRepo
    {
        TransactionResult Add(WorkItem x);
        List<WorkItem> GetAll();
        WorkItem Get(Guid id);
        WorkItem Get(string id);
        TransactionResult Update(WorkItem x);
        TransactionResult Delete(Guid id);
        TransactionResult Delete(string id);
    }
}
