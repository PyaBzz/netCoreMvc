using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using Dapper;

namespace myCoreMvc.Persistence
{
    public class WorkplanRepo : IWorkplanRepo
    {
        private readonly IDbConFactory dbConFactory;
        public WorkplanRepo(IDbConFactory conFac)
        {
            dbConFactory = conFac;
        }

        /*==================================  Interface Methods =================================*/

        public WorkPlan Add(WorkPlan wp)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"INSERT INTO WorkPlans (Id, Name) VALUES (@Id, @Name)", wp);
                return wp;
            }
        }

        public List<WorkPlan> GetAll()
        {
            using (var conn = dbConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM WorkPlans");
                return reader.Read<WorkPlan>().ToList();
            }

        }

        public WorkPlan Get(string id)
        {
            using (var conn = dbConFactory.Get())
            {
                try
                {
                    return conn.QuerySingle<WorkPlan>($"SELECT * FROM WorkPlans WHERE Id = @Id", new { Id = id });
                }
                catch
                {
                    return null;
                }
            }
        }

        public WorkPlan Get(Guid id) => Get(id.ToString());

        public WorkPlan Update(WorkPlan wp)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"UPDATE WorkPlans SET Name = @Name WHERE Id = @Id", wp);
                return wp;
            }
        }

        public void Delete(string id)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"DELETE FROM WorkPlans WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid id) => Delete(id.ToString());
    }
}
