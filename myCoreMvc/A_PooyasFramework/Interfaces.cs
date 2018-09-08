using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public interface IClonable
    {
    }

    public interface IDataProvider
    {
        List<T> GetList<T>();
        List<T> GetList<T>(Func<T, bool> func);
        T Get<T>(Func<T, bool> func);
        T Get<T>(Guid id) where T : Thing;
        T Get<T>(string id) where T : Thing;
        TransactionResult Add<T>(T obj) where T : Thing;
        TransactionResult Update<T>(T obj) where T : Thing;
        TransactionResult Delete<T>(Guid id) where T : Thing;
    }

    public interface IUserService
    {
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
        TransactionResult Save(User user); //Task: Add delete too
        TransactionResult SetPassword(Guid id, string password);
    }
}
