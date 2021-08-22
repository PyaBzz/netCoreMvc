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
    // public class WorkplanRepoMock : IWorkplanRepo
    // {
    //     private List<WorkPlan> Data;

    //     public WorkplanRepoMock()
    //     {
    //         var conf = ConfigFactory.Get();
    //         var xmlReader = new XmlReader<WorkPlan>(conf);
    //         Data = xmlReader.Read();
    //     }

    //     /*==================================  Interface Methods =================================*/

    //     public WorkPlan Add(WorkPlan wp)
    //     {
    //         Data.Add(wp);
    //         return wp;
    //     }
    //     public List<WorkPlan> GetAll() => Data;
    //     public WorkPlan Get(Guid? id) => Data.SingleOrDefault(i => i.Id == id);
    //     public WorkPlan Get(string id) => Get(new Guid(id));

    //     public WorkPlan Update(WorkPlan wp)
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
