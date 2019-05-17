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
        List<User> GetList(); // Replace with a reference to User
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
        TransactionResult Save(User user);
        TransactionResult Delete(Guid id);
        TransactionResult SetPassword(Guid id, string password);
    }
}
