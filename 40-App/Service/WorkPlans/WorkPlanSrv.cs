using System.Collections.Generic;
using myCoreMvc.App.Interfaces;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public class WorkPlanSrv : IWorkPlanSrv
    {
        private IWorkplanRepo repo;

        public WorkPlanSrv(IWorkplanRepo r)
        {
            repo = r;
        }

        public List<WorkPlan> GetAll() => null;
    }
}
