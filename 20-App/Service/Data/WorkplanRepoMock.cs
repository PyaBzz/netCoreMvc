using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using myCoreMvc.Domain;
using Baz.Core;

namespace myCoreMvc.App.Services
{
    public class WorkplanRepoMock : IWorkplanRepo
    {
        private Config _Config;
        private List<WorkPlan> Data;

        public WorkplanRepoMock(Config conf)
        {
            this._Config = conf;
            var xmlReader = new XmlReader<WorkPlan>(conf);
            Data = xmlReader.Read();
        }

        /*==================================  Interface Methods =================================*/

        public TransactionResult Add(WorkPlan wp)
        {
            // wp.Id = Guid.NewGuid(); //Todo: Can we delegate Id generation to sql?
            Data.Add(wp);
            return TransactionResult.Added;
        }
        public List<WorkPlan> GetAll() => Data;
        public WorkPlan Get(Guid id) => Data.SingleOrDefault(i => i.Id == id);
        public WorkPlan Get(string id) => Get(new Guid(id));

        public TransactionResult Update(WorkPlan wp)
        {
            var target = Get(wp.Id);
            if (target == null)
                return TransactionResult.NotFound;
            else
            {
                target.Name = wp.Name;
                return TransactionResult.Updated;
            }
        }

        public TransactionResult Delete(Guid id)
        {
            var target = Data.SingleOrDefault(e => e.Id == id);
            if (target == null)
                return TransactionResult.NotFound;
            Data.Remove(target);
            return TransactionResult.Deleted;
        }
        public TransactionResult Delete(string id)
        {
            var guid = new Guid(id);
            return Delete(guid);
        }
    }
}
