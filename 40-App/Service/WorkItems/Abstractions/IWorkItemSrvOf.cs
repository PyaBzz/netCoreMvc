using myCoreMvc.Domain;

namespace myCoreMvc.App.Services
{
    public interface IWorkItemSrvOf
    {
        WorkItem WorkItem { get; }

        WorkItem Save();
        void Delete();
    }
}
