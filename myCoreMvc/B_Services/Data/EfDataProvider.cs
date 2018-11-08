using Microsoft.EntityFrameworkCore;
using myCoreMvc.Services;
using PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class EfDataProvider : IDataProvider
    {
        /*==================================  Fields =================================*/

        private readonly EfCtx Ctx;

        /*==================================  Methods =================================*/

        public EfDataProvider(EfCtx ctx)
        {
            Ctx = ctx;
        }

        /*==================================  Methods of IDataProvider =================================*/

        public TransactionResult Add<T>(T obj) where T : Thing
        {
            Ctx.Set<T>().Add(obj); //Task: We could simply say Ctx.Add(obj) and let it figure type and stuff
            return Ctx.SaveChanges() == 0 ? TransactionResult.Failed : TransactionResult.Added;
        }

        public T Get<T>(Guid id) where T : Thing => Ctx.Set<T>().Find(id); //Find() is more performant than GetList etc.

        public T Get<T>(Func<T, bool> func) where T : Thing => Ctx.Set<T>().SingleOrDefault(t => func(t)); //Single() is a Linq to Entity execution method

        public List<T> GetList<T>() where T : Thing => Ctx.Set<T>().ToList(); //ToList() is a Linq to Entity execution method

        public List<T> GetList<T>(Func<T, bool> func) where T : Thing => Ctx.Set<T>().Where(t => func(t)).ToList();

        public TransactionResult Update<T>(T obj) where T : Thing
        {
            Ctx.Set<T>().Update(obj);
            return Ctx.SaveChanges() == 0 ? TransactionResult.NotFound : TransactionResult.Updated;
        }

        public TransactionResult Delete<T>(Guid id) where T : Thing
        {
            var target = Get<T>(id); // To delete using a single DB trip, we can use Ctx.Database.ExecuteSqlCommand("exec DeleteById {0}", id) or DbSet.FromSql()
            Ctx.Set<T>().Remove(target);
            return Ctx.SaveChanges() == 0 ? TransactionResult.NotFound : TransactionResult.Deleted;
        }
    }
}
