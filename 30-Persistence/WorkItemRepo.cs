using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using Dapper;

namespace myCoreMvc.Persistence
{
    public class WorkItemRepo : IWorkItemRepo
    {
        private readonly IDbConFactory dbConFactory;

        public WorkItemRepo(IDbConFactory conFac)
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

        public WorkItem Save(WorkItem x)
        {
            return x.Id.HasValue ? Update(x) : Add(x);
        }

        public List<WorkItem> GetAll()
        {
            using (var conn = dbConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM WorkItems");
                return reader.Read<WorkItem>().ToList();
            }
        }

        public WorkItem Get(string id) => Get(new Guid(id));

        public WorkItem Get(Guid? id)
        {
            if (id.HasValue)
                using (var conn = dbConFactory.Get())
                {
                    try
                    {
                        return conn.QuerySingle<WorkItem>($"SELECT * FROM WorkItems WHERE Id = @Id", new { Id = id });
                    }
                    catch (Exception e)
                    {
                        if (e is InvalidOperationException)
                            return null;
                        else
                            throw;
                    }
                }
            else
                throw new Exception("The provided nullable GUID has no value");
        }

        public void Delete(string id)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"DELETE FROM WorkItems WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid? id) => Delete(id.ToString());

        public void DeleteAll()
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute("DELETE FROM WorkItems");
            }
        }
    }
}
