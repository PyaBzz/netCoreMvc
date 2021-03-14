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
    public class WorkItemRepoMock : IWorkItemRepo
    {
        private Config _Config;
        private List<WorkItem> Data;

        public WorkItemRepoMock(Config conf)
        {
            this._Config = conf;
            var xmlReader = new XmlReader<WorkItem>(conf);
            Data = xmlReader.Read();
        }

        /*==================================  Interface Methods =================================*/

        public TransactionResult Add(WorkItem obj)
        {
            // wp.Id = Guid.NewGuid(); //Todo: Can we delegate Id generation to sql?
            Data.Add(obj);
            return TransactionResult.Added;
        }
        public List<WorkItem> GetAll() => Data;
        public WorkItem Get(Guid id) => Data.SingleOrDefault(i => i.Id == id);
        public WorkItem Get(string id) => Get(new Guid(id));

        public TransactionResult Update(WorkItem obj)
        {
            var target = Get(obj.Id);
            if (target == null)
                return TransactionResult.NotFound;
            else
            {
                target.Name = obj.Name;
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

        public TransactionResult Delete(string id) => Delete(new Guid(id));
    }
}
