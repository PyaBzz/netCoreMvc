using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Dapper;

namespace myCoreMvc.App.Services
{
    public class UserRepo : IUserRepo
    {
        private User Add(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                var id = conn.ExecuteScalar<Guid>("INSERT INTO Users (Name, DateOfBirth, Role, Salt, Hash) OUTPUT INSERTED.Id VALUES (@Name, @DateOfBirth, @Role, @Salt, @Hash)", x);
                x.Id = id;
                return x;
            }
        }

        private User Update(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"UPDATE Users SET Name = @Name WHERE Id = @Id", x);
            }
            return x;
        }

        /*==================================  Interface Methods =================================*/

        public User Save(User x)
        {
            return x.Id.HasValue ? Update(x) : Add(x);
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
                    return conn.QuerySingle<User>($"SELECT * FROM Users WHERE Id = @Id", new { Id = new Guid(id) });
                }
                catch (Exception e)
                {
                    if (e is InvalidOperationException)
                        return null;
                    else
                        throw;
                }
            }
        }

        public User Get(Guid? id)
        {
            if (id.HasValue)
                return Get(id.Value.ToString());
            else
                throw new Exception("The provided nullable GUID has no value");
        }

        public void Delete(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid? id) => Delete(id.ToString());
    }
}
