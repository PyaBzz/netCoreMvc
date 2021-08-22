using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class WorkplanRepo : CrudRepo<WorkPlan>, IWorkplanRepo
    {
        public WorkplanRepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
