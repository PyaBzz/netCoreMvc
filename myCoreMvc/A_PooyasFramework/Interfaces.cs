using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PooyasFramework
{
    public interface IClonable
    {
    }

    public interface IDataProvider
    {
        List<T> GetList<T>() where T : Thing;
        List<T> GetList<T>(Func<T, bool> func) where T : Thing;
        T Get<T>(Func<T, bool> func) where T : Thing;
        T Get<T>(Guid id) where T : Thing;
        List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : Thing;
        List<T> GetListIncluding<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : Thing;
        //Lesson: Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
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
