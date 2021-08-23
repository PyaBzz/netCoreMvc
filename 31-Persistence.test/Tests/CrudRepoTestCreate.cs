using System;
using Xunit;

namespace myCoreMvc.Persistence.Test
{
    public partial class CrudRepoTest : IDisposable
    {
        /*==================================  Create  =================================*/

        [Fact]
        public void Save_Saves_Guid_Uninitialised_As_EmptyGuid()
        {
            Assert.StrictEqual(Guid.Empty, A1.RefId);
            var retrieved = SaveAndRetrieve(A1);
            Assert.StrictEqual(Guid.Empty, retrieved.RefId);
        }

        [Fact]
        public void Save_Saves_NullableGuid_Uninitialised_As_Null()
        {
            Assert.Null(A1.NullableRefId);
            var retrieved = SaveAndRetrieve(A1);
            Assert.Null(retrieved.NullableRefId);
        }

        [Fact]
        public void Save_AssignsIdToNewRecords()
        {
            Assert.Null(A1.Id);
            var retrieved = SaveAndRetrieve(A1);
            Assert.NotNull(A1.Id);
            Assert.StrictEqual(A1, retrieved);
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
