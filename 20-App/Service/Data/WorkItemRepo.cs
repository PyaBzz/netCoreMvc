﻿using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using Dapper;

namespace myCoreMvc.App.Services
{
    public class WorkItemRepo : IWorkItemRepo
    {
        /*==================================  Interface Methods =================================*/

        public WorkItem Add(WorkItem x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"INSERT INTO WorkItems (Id, Name) VALUES (@Id, @Name)", x);
                return x;
            }
        }

        public List<WorkItem> GetAll()
        {
            using (var conn = SqlConFactory.Get())
            {
                var reader = conn.QueryMultiple($"SELECT * FROM WorkItems");
                return reader.Read<WorkItem>().ToList();
            }

        }

        public WorkItem Get(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                return conn.QuerySingle<WorkItem>($"SELECT * FROM WorkItems WHERE Id = @Id", new { Id = id });
            }
        }

        public WorkItem Get(Guid id) => Get(id.ToString());

        public WorkItem Update(WorkItem x)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"UPDATE WorkItems SET Name = @Name WHERE Id = @Id", x);
                return x;
            }
        }

        public void Delete(string id)
        {
            using (var conn = SqlConFactory.Get())
            {
                conn.Execute($"DELETE FROM WorkItems WHERE Id = @Id", new { Id = id });
            }
        }

        public void Delete(Guid id) => Delete(id.ToString());
    }
}