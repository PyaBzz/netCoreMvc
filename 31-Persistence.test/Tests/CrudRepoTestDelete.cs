using System;
using Xunit;

namespace myCoreMvc.Persistence.Test
{
    public partial class CrudRepoTest : IDisposable
    {
        /*==================================  Delete  =================================*/

        [Fact]
        public void Delete_DeletesByGuid()
        {
            repo.Save(A1);
            repo.Delete(A1.Id);
            Assert.Null(repo.Get(A1.Id));
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Save(A1);
            repo.Delete(A1.Id.ToString());
            Assert.Null(repo.Get(A1.Id));
        }
    }
}
