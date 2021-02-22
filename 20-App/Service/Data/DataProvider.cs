using Dapper;
using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    public class DataProvider : IDataProvider
    {
        public T Get<T>(Guid id) where T : class, IThing
        {
            throw new NotImplementedException();
        }
        public T Get<T>(Func<T, bool> func) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public List<T> GetList<T>() where T : class, IThing
        {
            using (var conn = SqlConFactory.Get())
            {
                var reader = conn.QueryMultiple("select * from WorkPlans");
                return reader.Read<T>().ToList();
            }
        }

        public List<T> GetList<T>(Func<T, bool> func) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public List<T> GetListIncluding<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        #region Lesson
        //Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
        #endregion

        public TransactionResult Save<T>(T obj) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public TransactionResult Delete<T>(Guid id) where T : class, IThing
        {
            throw new NotImplementedException();
        }
    }
}
