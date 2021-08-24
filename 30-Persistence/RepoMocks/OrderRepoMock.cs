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
//     public class OrderRepoMock : IOrderRepo
//     {
//         private Config config;
//         private List<Order> Data;

//         public OrderRepoMock(Config conf)
//         {
//             this.config = conf;
//             var xmlReader = new XmlReader<Order>(conf);
//             Data = xmlReader.Read();
//         }

//         /*==================================  Interface Methods =================================*/

//         public Order Add(Order x)
//         {
//             Data.Add(x);
//             return x;
//         }

//         public List<Order> GetAll() => Data;

//         public Order Get(Guid id)
//         {
//             throw new NotImplementedException();
//             // Data.SingleOrDefault(i => i.Id == id);
//         }

//         public Order Get(string id) => Get(new Guid(id));

//         public Order Update(Order x)
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
