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
        T Get<T>(Guid id) where T : class, ISavable;
        T Get<T>(Predicate<T> func) where T : class, ISavable;
        List<T> GetList<T>(Predicate<T> predicate = null) where T : class, ISavable;
        List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, ISavable;
        List<T> GetListIncluding<T>(Predicate<T> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, ISavable;

        #region Lesson
        //Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
        #endregion

        T Save<T>(T obj) where T : class, ISavable;
        void Delete<T>(Guid id) where T : class, ISavable;
    }
}
