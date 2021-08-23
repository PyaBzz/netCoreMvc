using System;
using Xunit;

namespace myCoreMvc.Persistence.Test
{
    public partial class CrudRepoTest : IDisposable
    {
        /*==================================  Update  =================================*/

        [Fact]
        public void Save_PreservesIdOfExistingRecords()
        {
            Assert.False(A1.Id.HasValue);
            repo.Save(A1);
            var id1 = A1.Id.Value;
            repo.Save(A1);
            var id2 = A1.Id.Value;
            Assert.StrictEqual(id1, id2);
        }

        [Fact]
        public void Save_Updates_String()
        {
            var recordToChange = SaveAndRetrieve(A1);
            var originalName = recordToChange.Name;
            var newName = "updated";
            recordToChange.Name = newName;
            repo.Save(recordToChange);
            Assert.StrictEqual(newName, repo.Get(A1.Id).Name);
        }
    }
}
