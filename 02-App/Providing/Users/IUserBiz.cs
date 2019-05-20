using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Providing
{
    public interface IUserBiz //Task: Needs Interface Segregation as it has common aspects with IDataProvider
    {
        User Get(Guid id);
        List<User> GetList();
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
        IUserBizOf Of(User user);
    }

    public interface IUserBizOf
    {
        User User { get; }
        TransactionResult Save();
        TransactionResult SetPassword(string password);
        TransactionResult Delete();
    }
}
