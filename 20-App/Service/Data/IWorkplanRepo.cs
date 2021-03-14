using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IWorkplanRepo
    {
        TransactionResult Add(WorkPlan wp);
        List<WorkPlan> GetAll();
        WorkPlan Get(Guid id);
        WorkPlan Get(string id);
        TransactionResult Update(WorkPlan wp);
        TransactionResult Delete(Guid id);
        TransactionResult Delete(string id);
    }
}
