using System;
using Xunit;

namespace myCoreMvc.Persistence.Test
{
    [Trait("Group", "CrudRepo")]
    public partial class CrudRepoTest : IDisposable
    { //Todo: Cover all CRUD related operations
        private readonly CrudRepo<DummyA> repo;
        private readonly DummyA
            A1 = new DummyA { Name = "A1" },
            A2 = new DummyA { Name = "A2" },
            A3 = new DummyA { Name = "A3" },
            A4 = new DummyA { Name = "A4" };

        public CrudRepoTest(CrudRepo<DummyA> rep)
        {
            this.repo = rep;
        }

        public void Dispose()
        {
            repo.DeleteAll();
        }

        private DummyA SaveAndRetrieve(DummyA x)
        {
            repo.Save(x);
            return repo.Get(x.Id);
        }
    }
}
