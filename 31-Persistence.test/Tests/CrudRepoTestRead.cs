using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace myCoreMvc.Persistence.Test
{
    public partial class CrudRepoTest : IDisposable
    {
        /*==================================  Read  =================================*/

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<DummyA>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            List<DummyA> all;
            Assert.StrictEqual(0, repo.GetAll().Count());

            repo.Save(A1);
            all = repo.GetAll();
            Assert.StrictEqual(1, all.Count());
            Assert.Contains(A1, all);

            repo.Save(A2);
            all = repo.GetAll();
            Assert.StrictEqual(2, all.Count());
            Assert.Contains(A1, all);
            Assert.Contains(A2, all);

            repo.Save(A3);
            all = repo.GetAll();
            Assert.StrictEqual(3, all.Count());
            Assert.Contains(A1, all);
            Assert.Contains(A2, all);
            Assert.Contains(A3, all);
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            repo.Save(A1);
            Assert.StrictEqual(A1, repo.Get(A1.Id));
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            repo.Save(A1);
            Assert.StrictEqual(A1, repo.Get(A1.Id.ToString()));
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            repo.Save(A1);
            Assert.StrictEqual(repo.Get(A1.Id.ToString().ToLower()), repo.Get(A1.Id.ToString().ToUpper()));
        }

        [Fact]
        public void Get_ReturnsNewObject()
        {
            repo.Save(A1);
            var retrievedObject = repo.Get(A1.Id);
            Assert.StrictEqual(A1, retrievedObject);
            Assert.NotSame(A1, retrievedObject);
        }
    }
}
