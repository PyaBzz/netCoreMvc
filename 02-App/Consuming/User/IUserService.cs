using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Consuming
{
    public interface IUserService //Task: Needs Interface Segregation as it has common aspects with IDataProvider
    {
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
        TransactionResult Save(User user); //Task: Add delete too
        TransactionResult SetPassword(Guid id, string password);
    }
}
