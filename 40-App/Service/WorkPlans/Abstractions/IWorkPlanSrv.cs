using myCoreMvc.Domain;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IWorkPlanSrv
    {
        List<WorkPlan> GetAll();
    }
}
