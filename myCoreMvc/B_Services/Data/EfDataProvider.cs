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

        private DbSet<T> GetDbSet<T>() where T : Thing
        {
            var targetPropertyInfo = Ctx.GetType().GetPublicDeclaredInstancePropertyInfos()
                .Single(pi => pi.PropertyType == typeof(DbSet<T>));
            return targetPropertyInfo.GetValue(Ctx) as DbSet<T>;
        }

        /*==================================  Methods of IDataProvider =================================*/

        public TransactionResult Add<T>(T obj) where T : Thing
        {
            throw new NotImplementedException();
        }

        public TransactionResult Delete<T>(Guid id) where T : Thing
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(Guid id) where T : Thing
        {
            throw new NotImplementedException();
        }

        public List<T> GetList<T>() where T : Thing
        {
            return GetDbSet<T>() as List<T>;
        }

        public List<T> GetList<T>(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public TransactionResult Update<T>(T obj) where T : Thing
        {
            throw new NotImplementedException();
        }
    }
}
