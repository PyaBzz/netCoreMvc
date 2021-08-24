using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Services
{
    #region Lesson
    //Although this interface has common aspects with
    //IDataRepo we don't use IDataRepo directly in UI
    #endregion

    public interface IUserSrv
    {
        User Get(Guid id);
        List<User> GetList();
        Task<bool> GetPrincipal(string userName, string passWord, out ClaimsPrincipal claimsPrincipal);
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);

        IUserSrvOf Of(User user);
    }
}
