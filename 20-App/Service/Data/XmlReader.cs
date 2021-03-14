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
    public class XmlReader<T> where T : class
    {
        private Config _Config;
        private List<WorkPlan> Data;

        public XmlReader(Config conf)
        {
            this._Config = conf;
        }

        public List<T> Read()
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
    }
}
