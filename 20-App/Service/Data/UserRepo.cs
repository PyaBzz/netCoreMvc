using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Dapper;

namespace myCoreMvc.App.Services
{
    public class UserRepo : IUserRepo
    {
        /*==================================  Interface Methods =================================*/

        public TransactionResult Add(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                try
                {
                    conn.Execute($"INSERT INTO Users (Id, Name) VALUES (@Id, @Name)", x);
                    return TransactionResult.Added;
                }
                catch
                {
                    return TransactionResult.Failed;
                }
            }
        }

        public List<User> GetAll()
        {
            using (var conn = SqlConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM Users");
                return reader.Read<User>().ToList();
            }

        }

        public User Get(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                try
                {
                    return conn.QuerySingle<User>($"SELECT * FROM Users WHERE Id = @Id", new { Id = id });
                }
                catch
                {
                    return null;
                }
            }
        }

        public User Get(Guid id) => Get(id.ToString());

        public TransactionResult Update(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"UPDATE Users SET Name = @Name WHERE Id = @Id", x);
            }
            return TransactionResult.Updated;
        }

        public TransactionResult Delete(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
            return TransactionResult.Deleted;
        }

        public TransactionResult Delete(Guid id) => Delete(id.ToString());
    }
}
