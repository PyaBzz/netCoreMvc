using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Providing
{
    #region Lesson
    //Lesson: Although this interface has common aspects with
    //IDataProvider we don't use IDataProvider directly in UI
    #endregion
    public interface IUserBiz
    {
        User Get(Guid id);
        List<User> GetList();
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
        IUserBizOf Of(User user);
    }
}
