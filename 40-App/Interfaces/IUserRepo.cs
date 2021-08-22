using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Interfaces
{
    public interface IUserRepo : ICrudRepo<User>
    {
    }
}
