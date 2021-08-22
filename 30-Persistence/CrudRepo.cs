using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Dapper;
using myCoreMvc.Persistence.Services;

namespace myCoreMvc.Persistence
{
    public abstract class CrudRepo<T> : ICrudRepo<T> where T : class, ISavable
    {
        private readonly Dictionary<Type, string> tableNames = new Dictionary<Type, string> {
            {typeof(User), "Users"},
            {typeof(WorkPlan), "WorkPlans"},
            {typeof(WorkItem), "WorkItems"}
            };

        private readonly IDbConFactory dbConFactory;

        public CrudRepo(IDbConFactory conFac)
        {
            dbConFactory = conFac;
        }

        private T Add(T x)
        {
            var colMap = new ColumnMap<T>();
            var cols = colMap.Get();
            var placeHolders = colMap.GetPlaceholders();
            var query = $"INSERT INTO {tableNames[typeof(T)]} ({cols}) OUTPUT INSERTED.Id VALUES ({placeHolders})";
            using (var conn = dbConFactory.Get())
            {
                var id = conn.ExecuteScalar<Guid>(query, x);
                x.Id = id;
                return x;
            }
        }

        private T Update(T x)
        {
            var colMap = new ColumnMap<T>();
            var assignments = colMap.GetAssignments();
            var query = $"UPDATE {tableNames[typeof(T)]} SET {assignments} WHERE Id = @Id";
            using (var conn = dbConFactory.Get())
            {
                conn.Execute(query, x);
            }
            return x;
        }

        /*==================================  Interface Methods =================================*/

        public T Save(T x)
        {
            return x.Id.HasValue ? Update(x) : Add(x);
        }

        public List<T> GetAll()
        {
            using (var conn = dbConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM {tableNames[typeof(T)]}");
                return reader.Read<T>().ToList();
            }
        }

        public T Get(string id) => Get(new Guid(id));

        public T Get(Guid? id)
        {
            if (id.HasValue)
                using (var conn = dbConFactory.Get())
                {
                    try
                    {
                        return conn.QuerySingle<T>($"SELECT * FROM {tableNames[typeof(T)]} WHERE Id = @Id", new { Id = id });
                    }
                    catch (Exception e)
                    {
                        if (e is InvalidOperationException)
                            return null;
                        else
                            throw;
                    }
                }
            else
                throw new Exception("The provided nullable GUID has no value");
        }

        public void Delete(string id)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"DELETE FROM {tableNames[typeof(T)]} WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid? id) => Delete(id.ToString());

        public void DeleteAll()
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"DELETE FROM {tableNames[typeof(T)]}");
            }
        }
    }
}
