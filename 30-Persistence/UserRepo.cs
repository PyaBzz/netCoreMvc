using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App;
using myCoreMvc.App.Interfaces;
using Dapper;

namespace myCoreMvc.Persistence
{
    public class UserRepo : CrudRepo<User>, IUserRepo
    {
        private readonly IDbConFactory dbConFactory;

        public UserRepo(IDbConFactory conFac) : base(conFac)
        {
            dbConFactory = conFac;
        }

        private User Add(User x)
        {
            using (var conn = dbConFactory.Get())
            {
                var id = conn.ExecuteScalar<Guid>("INSERT INTO Users (Name, DateOfBirth, Role, Salt, Hash) OUTPUT INSERTED.Id VALUES (@Name, @DateOfBirth, @Role, @Salt, @Hash)", x);
                x.Id = id;
                return x;
            }
        }

        private User Update(User x)
        {
            using (var conn = dbConFactory.Get())
            {
                conn.Execute($"UPDATE Users SET Name = @Name, DateOfBirth = @DateOfBirth, Role = @Role, Salt = @Salt, Hash = @Hash WHERE Id = @Id", x);
            }
            return x;
        }

        /*==================================  Interface Methods =================================*/

        public override User Save(User x)
        {
            return x.Id.HasValue ? Update(x) : Add(x);
        }
    }
}
