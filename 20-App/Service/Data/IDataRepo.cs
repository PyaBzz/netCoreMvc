using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    public interface IDataRepo
    {
        T Get<T>(Guid id) where T : class, IThing;
        T Get<T>(Func<T, bool> func) where T : class, IThing;

        List<T> GetList<T>() where T : class, IThing;
        List<T> GetList<T>(Func<T, bool> func) where T : class, IThing;
        List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;
        List<T> GetListIncluding<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;

        #region Lesson
        //Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
        #endregion

        TransactionResult Save<T>(T obj) where T : class, IThing;
        TransactionResult Delete<T>(Guid id) where T : class, IThing;
    }
}
