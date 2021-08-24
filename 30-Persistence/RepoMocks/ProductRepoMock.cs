using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using myCoreMvc.Domain;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Baz.Core;

namespace myCoreMvc.Persistence
{
    // public class ProductRepoMock : IProductRepo
    // {
    //     private List<Product> Data;

    //     public ProductRepoMock()
    //     {
    //         var conf = ConfigFactory.Get();
    //         var xmlReader = new XmlReader<Product>(conf);
    //         Data = xmlReader.Read();
    //     }

    //     /*==================================  Interface Methods =================================*/

    //     public Product Add(Product wp)
    //     {
    //         Data.Add(wp);
    //         return wp;
    //     }
    //     public List<Product> GetAll() => Data;
    //     public Product Get(Guid? id) => Data.SingleOrDefault(i => i.Id == id);
    //     public Product Get(string id) => Get(new Guid(id));

    //     public Product Update(Product wp)
    //     {
    //         var target = Get(wp.Id);
    //         if (target == null)
    //             throw new Exception("Not found");
    //         else
    //         {
    //             target.Name = wp.Name;
    //             return wp;
    //         }
    //     }

    //     public void Delete(Guid id)
    //     {
    //         var target = Data.SingleOrDefault(e => e.Id == id);
    //         if (target == null)
    //             throw new Exception("Not found");
    //         Data.Remove(target);
    //     }
    //     public void Delete(string id)
    //     {
    //         var guid = new Guid(id);
    //         Delete(guid);
    //     }
    // }
}
