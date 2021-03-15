using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Services
{
    public interface IUserRepo
    {
        TransactionResult Add(User x);
        List<User> GetAll();
        User Get(Guid id);
        User Get(string id);
        TransactionResult Update(User x);
        TransactionResult Delete(Guid id);
        TransactionResult Delete(string id);
    }
}
