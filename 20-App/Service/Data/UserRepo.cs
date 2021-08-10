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

        public User Add(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"INSERT INTO Users (Id, Name) VALUES (@Id, @Name)", x);
                return x;
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
                catch (Exception e)
                {
                    if (e is InvalidOperationException)
                        return null;
                    else
                        throw;
                }
            }
        }

        public User Get(Guid id) => Get(id.ToString());

        public User Update(User x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"UPDATE Users SET Name = @Name WHERE Id = @Id", x);
            }
            return x;
        }

        public void Delete(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid id) => Delete(id.ToString());
    }
}
