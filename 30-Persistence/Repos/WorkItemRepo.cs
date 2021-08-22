using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class WorkItemRepo : CrudRepo<WorkItem>, IWorkItemRepo
    {
        public WorkItemRepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
