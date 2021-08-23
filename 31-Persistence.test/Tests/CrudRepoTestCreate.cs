using System;
using Xunit;

namespace myCoreMvc.Persistence.Test
{
    public partial class CrudRepoTest : IDisposable
    {
        /*==================================  Create  =================================*/

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.False(A1.Id.HasValue);
            var retrieved = SaveAndRetrieve(A1);
            Assert.True(A1.Id.HasValue);
            Assert.StrictEqual(A1, repo.Get(A1.Id));
        }

        [Fact]
        public void Save_AssignsUniqueIds()
        {
            repo.Save(A1);
            repo.Save(A2);
            Assert.NotEqual(A1.Id.Value, A2.Id.Value);
        }

        [Fact]
        public void Save_Saves_String_Uninitialised_As_Null()
        {
            var x = new DummyA();
            Assert.Null(x.Name);
            var retrieved = SaveAndRetrieve(x);
            Assert.Null(retrieved.Name);
        }

        [Fact]
        public void Save_Saves_String_Empty_As_Empty()
        {
            var x = new DummyA { Name = string.Empty };
            Assert.Empty(x.Name);
            Assert.StrictEqual(string.Empty, x.Name);
            var retrieved = SaveAndRetrieve(x);
            Assert.StrictEqual(string.Empty, retrieved.Name);
        }

        [Fact]
        public void Save_Saves_String_Correctly()
        {
            var retrieved = SaveAndRetrieve(A1);
            Assert.StrictEqual(A1.Name, retrieved.Name);
        }
    }
}
