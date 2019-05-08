using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App
{
    public enum TransactionResult { Added, Updated, Deleted, NotFound, Failed }

    public interface IDataProvider
    {
        List<T> GetList<T>() where T : class, IThing;
        List<T> GetList<T>(Func<T, bool> func) where T : class, IThing;
        T Get<T>(Func<T, bool> func) where T : class, IThing;
        T Get<T>(Guid id) where T : class, IThing;
        List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;
        List<T> GetListIncluding<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;
        //Lesson: Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
        TransactionResult Add<T>(T obj) where T : class, IThing;
        TransactionResult Update<T>(T obj) where T : class, IThing;
        TransactionResult Delete<T>(Guid id) where T : class, IThing;
    }

    public interface IUserService //Task: Needs Interface Segregation as it has common aspects with IDataProvider
    {
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out IUser user);
        TransactionResult Save(IUser user); //Task: Add delete too
        TransactionResult SetPassword(Guid id, string password);
    }
}
