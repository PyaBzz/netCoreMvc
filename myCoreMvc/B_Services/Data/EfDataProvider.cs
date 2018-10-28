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
        private readonly EfCtx Ctx;

        public EfDataProvider(EfCtx ctx)
        {
            Ctx = ctx;
        }

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

        public List<T> GetList<T>()
        {
            throw new NotImplementedException();
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
