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
    public class DataRepo : IDataRepo
    {
        private readonly Dictionary<Type, string> TableNames = new Dictionary<Type, string>{
            {typeof(WorkPlan), "WorkPlans"},
            {typeof(WorkItem), "WorkItems"},
            {typeof(User), "Users"}
        };

        public T Get<T>(Guid id) where T : class, IThing
        {
            using (var conn = SqlConFactory.Get())
            {
                var tableName = TableNames[typeof(T)];
                var reader = conn.QuerySingle<T>($"SELECT * FROM {tableName} WHERE Id = @Id", new { Id = id.ToString() });
                return reader;
            }
        }
        public T Get<T>(Predicate<T> func) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public List<T> GetList<T>(Predicate<T> predicate = null) where T : class, IThing
        {
            if (predicate == null)
            {
                using (var conn = SqlConFactory.Get())
                {
                    var reader = conn.QueryMultiple($"SELECT * FROM {typeof(T).Name}s");
                    return reader.Read<T>().ToList();
                }
            }
            else
                throw new NotImplementedException();
        }

        public List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
        {
            throw new NotImplementedException();
        }

        public List<T> GetListIncluding<T>(Predicate<T> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing
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
