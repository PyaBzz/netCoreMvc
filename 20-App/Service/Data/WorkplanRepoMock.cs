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
            Data = ReadFromXml<WorkPlan>();
        }

        private List<T> ReadFromXml<T>() where T : class //Todo: Make reusable in all mocked repos
        {
            var res = new List<T>();
            var outputDir = Assembly.GetExecutingAssembly().GetDirectory();
            var typeName = typeof(T).Name;
            try
            {
                var sourceFilePath = Path.Combine(outputDir, _Config.Data.Path.XmlSource[typeName + "s"]);
                var serialiser = new XmlSerializer(typeof(T));

                using (var stream = File.OpenRead(sourceFilePath))
                {
                    using (var xmlRdr = XmlReader.Create(stream))
                    {
                        while (xmlRdr.Read())
                        {
                            if (xmlRdr.NodeType == XmlNodeType.Element && xmlRdr.Name == typeName)
                            {
                                var workPlan = serialiser.Deserialize(xmlRdr) as T;
                                res.Add(workPlan);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
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
