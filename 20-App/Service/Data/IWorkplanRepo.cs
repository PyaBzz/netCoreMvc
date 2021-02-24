using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    public interface IWorkplanRepo
    {
        TransactionResult Add(WorkPlan obj);
        List<WorkPlan> GetAll();
        WorkPlan Get(Guid id);
        WorkPlan Get(string id);
        TransactionResult Delete(Guid id);
        TransactionResult Delete(string id);
    }
}
