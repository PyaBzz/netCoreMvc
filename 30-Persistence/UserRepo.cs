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
    public class UserRepo : CrudRepo<User>, IUserRepo
    {
        private readonly IDbConFactory dbConFactory;

        public UserRepo(IDbConFactory conFac) : base(conFac)
        {
            dbConFactory = conFac;
        }
    }
}
