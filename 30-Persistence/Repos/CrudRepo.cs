using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using Dapper;
using myCoreMvc.Persistence.Services;

namespace myCoreMvc.Persistence
{
    public abstract class CrudRepo<T> : ICrudRepo<T> where T : class, ISavable
    {
        private readonly DbMap<T> dbMap = new DbMap<T>();
        private readonly IDbConFactory dbConFactory;

        public CrudRepo(IDbConFactory conFac)
        {
            dbConFactory = conFac;
        }

        private T Add(T x)
        {
            var cols = dbMap.GetColumns();
            var placeHolders = dbMap.GetPlaceholders();
            var query = $"INSERT INTO {dbMap.Table} ({cols}) OUTPUT INSERTED.Id VALUES ({placeHolders})";
            using (var conn = dbConFactory.Get())
            {
                var id = conn.ExecuteScalar<Guid>(query, x);
                x.Id = id;
                return x;
            }
        }

        private T Update(T x)
        {
            var assignments = dbMap.GetAssignments();
            var query = $"UPDATE {dbMap.Table} SET {assignments} WHERE Id = @Id";
            using (var conn = dbConFactory.Get())
            {
                conn.Execute(query, x);
            }
            return x;
        }

        /*==================================  Interface Methods =================================*/

        public T Save(T x)
        {
            x.Validate();
            return x.Id.HasValue ? Update(x) : Add(x);
        }

        public List<T> GetAll()
        {
            using (var conn = dbConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM {dbMap.Table}");
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
                        return conn.QuerySingle<T>($"SELECT * FROM {dbMap.Table} WHERE Id = @Id", new { Id = id });
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
                conn.Execute($"DELETE FROM {dbMap.Table} WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid? id) => Delete(id.ToString());

        public void DeleteAll()
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"DELETE FROM {dbMap.Table}");
            }
        }
    }
}
