using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using myCoreMvc.Domain;
using Baz.Core;
using Dapper;

namespace myCoreMvc.App.Services
{
    public class WorkplanRepo : IWorkplanRepo
    {
        /*==================================  Interface Methods =================================*/

        public TransactionResult Save(WorkPlan wp)
        {
            throw new NotImplementedException();
        }
        public List<WorkPlan> GetAll()
        {
            using (var conn = SqlConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM WorkPlans");
                return reader.Read<WorkPlan>().ToList();
            }

        }
        public WorkPlan Get(Guid id)
        {
            using (var conn = SqlConFactory.Get())
            {
                return conn.QuerySingle<WorkPlan>($"SELECT * FROM WorkPlans WHERE Id = @Id", new { Id = id.ToString() });
            }
        }
        public WorkPlan Get(string id)
            => Get(new Guid(id));
        public TransactionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public TransactionResult Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
