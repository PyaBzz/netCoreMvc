using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IUserRepo
    {
        User Add(User x);  //Todo: Should the DB/repo generate ID's and we return them here?
        List<User> GetAll();
        User Get(Guid id);
        User Get(string id);
        User Update(User x);
        void Delete(Guid id);
        void Delete(string id);
    }
}
