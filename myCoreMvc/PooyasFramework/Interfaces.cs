using myCoreMvc.PooyasFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public interface IClonable
    {
    }

    public interface IDataProvider
    {
        List<T> GetList<T>();
        List<T> GetList<T>(Func<T, bool> func);
        T Get<T>(Func<T, bool> func);
        T Get<T>(Guid id) where T : Thing;
        T Get<T>(string id) where T : Thing;
        TransactionResult Add<T>(T obj) where T : Thing;
        TransactionResult Update<T>(T obj) where T : Thing;
        TransactionResult Delete<T>(Guid id) where T : Thing;
    }
}
