using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Providing
{
    public interface IUserBizOf
    {
        User User { get; }
        TransactionResult Save();
        TransactionResult SetPassword(string password);
        TransactionResult Delete();
    }
}
