using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;

namespace myCoreMvc.Persistence
{
    public class DummyARepo : CrudRepo<DummyA>
    {
        public DummyARepo(IDbConFactory conFac) : base(conFac)
        {
        }
    }
}
