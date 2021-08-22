using System;
using System.Collections.Generic;
using System.Linq;
using myCoreMvc.Domain;
using myCoreMvc.App.Interfaces;
using Dapper;
using myCoreMvc.Persistence.Services;
using Baz.Core;

namespace myCoreMvc.Persistence
{
    public class WorkItemRepo : CrudRepo<WorkItem>, IWorkItemRepo
    {
        private readonly IDbConFactory dbConFactory;

        public WorkItemRepo(IDbConFactory conFac) : base(conFac)
        {
            dbConFactory = conFac;
        }
    }
}
