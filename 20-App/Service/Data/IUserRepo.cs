using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IUserRepo
    {
        User Save(User x);
        List<User> GetAll();
        User Get(Guid? id);
        User Get(string id);
        void Delete(Guid? id);
        void Delete(string id);

        void DeleteAll();
    }
}
