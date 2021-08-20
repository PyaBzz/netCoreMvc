using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using Dapper;

namespace myCoreMvc.Persistence
{
    public class WorkItemRepo : CrudRepo<WorkItem>, IWorkItemRepo
    {
        private readonly IDbConFactory dbConFactory;

        public WorkItemRepo(IDbConFactory conFac) : base(conFac)
        {
            dbConFactory = conFac;
        }

        private WorkItem Add(WorkItem x)
        {
            using (var conn = dbConFactory.Get())
            {
                var id = conn.ExecuteScalar<Guid>($"INSERT INTO WorkItems (Name) OUTPUT INSERTED.Id VALUES (@Name)", x);
                x.Id = id;
                return x;
            }
        }

        private WorkItem Update(WorkItem x)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"UPDATE WorkItems SET Name = @Name WHERE Id = @Id", x);
            }
            return x;
        }

        /*==================================  Interface Methods =================================*/

        public override WorkItem Save(WorkItem x)
        {
            return x.Id.HasValue ? Update(x) : Add(x);
        }
    }
}
