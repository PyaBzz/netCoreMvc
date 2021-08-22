using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class UserRepo : CrudRepo<User>, IUserRepo
    {
        public UserRepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
