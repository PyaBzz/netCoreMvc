using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Dapper;

namespace myCoreMvc.App.Services
{
    public class WorkItemRepo : IWorkItemRepo
    {
        /*==================================  Interface Methods =================================*/

        public TransactionResult Add(WorkItem obj)
        {
            using (var conn = SqlConFactory.Get())
            {
                try
                {
                    conn.Execute($"INSERT INTO WorkItems (Id, Name) VALUES (@Id, @Name)", obj);
                    return TransactionResult.Added;
                }
                catch
                {
                    return TransactionResult.Failed;
                }
            }
        }

        public List<WorkItem> GetAll()
        {
            using (var conn = SqlConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM WorkItems");
                return reader.Read<WorkItem>().ToList();
            }

        }

        public WorkItem Get(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                try
                {
                    return conn.QuerySingle<WorkItem>($"SELECT * FROM WorkItems WHERE Id = @Id", new { Id = id });
                }
                catch
                {
                    return null;
                }
            }
        }

        public WorkItem Get(Guid id) => Get(id.ToString());

        public TransactionResult Update(WorkItem wp)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"UPDATE WorkItems SET Name = @Name WHERE Id = @Id", wp);
            }
            return TransactionResult.Updated;
        }

        public TransactionResult Delete(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"DELETE FROM WorkItems WHERE Id = @Id", new { Id = id });
            }
            return TransactionResult.Deleted;
        }

        public TransactionResult Delete(Guid id) => Delete(id.ToString());
    }
}
