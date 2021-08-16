using System;
using System.Collections.Generic;
using myCoreMvc.Domain;

namespace myCoreMvc.App.Interfaces
{
    public interface IWorkItemRepo
    {
        WorkItem Save(WorkItem x);
        List<WorkItem> GetAll();
        WorkItem Get(Guid? id);
        WorkItem Get(string id);
        void Delete(Guid? id);
        void Delete(string id);
        void DeleteAll();
    }
}
