using System;
using Xunit;
using myCoreMvc.App.Services;
using myCoreMvc.Domain;
using System.Linq;
using System.Collections.Generic;
using myCoreMvc.App;

namespace myCoreMvc.Test.DataLayer
{
    [Trait("Group", "Data")]
    public class WorkItemRepoTest
    {
        private readonly Dictionary<string, string> _ids = new Dictionary<string, string>{
            {"Wi11", "7073ad87-4695-4a0b-b2c3-fa794d5ffa21"},
            {"Wi12", "5fc4bfcf-24e0-430a-8889-03b2f31387e1"},
            {"Wi21", "eb66287b-1cde-421e-868e-a0df5b21a90d"},
            {"Wi22", "19394153-a745-4b2a-9889-bf28237bd3d8"},
        };

        private readonly IWorkItemRepo repo;

        public WorkItemRepoTest(IWorkItemRepo rep)
        {
            this.repo = rep;
        }

        [Fact]
        public void Add_Adds()
        {
            var newRef = "Wi22";
            var newId = _ids[newRef];
            Assert.Null(repo.Get(newId));
            var newName = "FourthItem";
            var newItem = new WorkItem
            {
                Id = new Guid(newId),
                Name = newName,
                Reference = newRef
            };
            Assert.StrictEqual(newItem, repo.Add(newItem));
            Assert.StrictEqual(4, repo.GetAll().Count());
            Assert.StrictEqual(newName, repo.Get(newId).Name);
            repo.Delete(newId);
            Assert.StrictEqual(3, repo.GetAll().Count());
        }

        [Fact]
        public void GetAll_GetsTheRightType()
        {
            Assert.IsType<List<WorkItem>>(repo.GetAll());
        }

        [Fact]
        public void GetAll_GetsAllItems()
        {
            Assert.StrictEqual(3, repo.GetAll().Count());
        }

        [Fact]
        public void Get_GetsByStringId()
        {
            Assert.StrictEqual("SecondItem", repo.Get(_ids["Wi12"]).Name);
        }

        [Fact]
        public void Get_GetsByGuid()
        {
            Assert.StrictEqual("SecondItem", repo.Get(new Guid(_ids["Wi12"])).Name);
        }

        [Fact]
        public void Get_IsCaseInsensitiveToId()
        {
            Assert.StrictEqual(repo.Get(_ids["Wi12"]), repo.Get(_ids["Wi12"].ToUpper()));
        }

        [Fact]
        public void Update_UpdatesName()
        {
            var wiToChange = repo.Get(_ids["Wi11"]);
            var originalName = wiToChange.Name;
            var newName = "updated";
            wiToChange.Name = newName;
            Assert.StrictEqual(wiToChange, repo.Update(wiToChange));
            Assert.StrictEqual(newName, repo.Get(_ids["Wi11"]).Name);
            wiToChange.Name = originalName;
            Assert.StrictEqual(wiToChange, repo.Update(wiToChange));
            Assert.StrictEqual(originalName, repo.Get(_ids["Wi11"]).Name);
        }

        [Fact]
        public void Delete_DeletesByStringId()
        {
            repo.Delete(_ids["Wi12"]);
            Assert.Null(repo.Get(_ids["Wi12"]));
            Assert.StrictEqual(2, repo.GetAll().Count());
            var newItem = new WorkItem
            {
                Id = new Guid(_ids["Wi12"]),
                Name = "SecondItem"
            };
            Assert.StrictEqual(newItem, repo.Add(newItem));
            Assert.StrictEqual(3, repo.GetAll().Count());
        }
    }
}
