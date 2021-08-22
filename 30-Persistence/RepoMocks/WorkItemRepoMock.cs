// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Reflection;
// using System.Xml;
// using System.Xml.Serialization;
// using myCoreMvc.Domain;
// using myCoreMvc.App;
// using myCoreMvc.App.Interfaces;
// using Baz.Core;

// namespace myCoreMvc.Persistence
// {
//     public class WorkItemRepoMock : IWorkItemRepo
//     {
//         private Config config;
//         private List<WorkItem> Data;

//         public WorkItemRepoMock(Config conf)
//         {
//             this.config = conf;
//             var xmlReader = new XmlReader<WorkItem>(conf);
//             Data = xmlReader.Read();
//         }

//         /*==================================  Interface Methods =================================*/

//         public WorkItem Add(WorkItem x)
//         {
//             Data.Add(x);
//             return x;
//         }

//         public List<WorkItem> GetAll() => Data;

//         public WorkItem Get(Guid id)
//         {
//             throw new NotImplementedException();
//             // Data.SingleOrDefault(i => i.Id == id);
//         }

//         public WorkItem Get(string id) => Get(new Guid(id));

//         public WorkItem Update(WorkItem x)
//         {
//             throw new NotImplementedException();

//             // var target = Get(x.Id);
//             // if (target == null)
//             //     throw new Exception("Not found");
//             // else
//             // {
//             //     target.Name = x.Name;
//             //     return x;
//             // }
//         }

//         public void Delete(Guid id)
//         {
//             var target = Data.SingleOrDefault(e => e.Id == id);
//             if (target == null)
//                 throw new Exception("Not found");
//             Data.Remove(target);
//         }

//         public void Delete(string id) => Delete(new Guid(id));
//     }
// }
