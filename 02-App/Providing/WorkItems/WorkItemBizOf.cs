using myCoreMvc.App.Consuming;
using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace myCoreMvc.App.Providing
{
    public class WorkItemBizOf : IWorkItemBizOf
    {
        private readonly IDataProvider DataProvider;
        public WorkItem WorkItem { get; }

        public WorkItemBizOf(IDataProvider dataProvider, WorkItem workItem)
        {
            DataProvider = dataProvider;
            WorkItem = workItem;
        }

        TransactionResult IWorkItemBizOf.Add() => DataProvider.Add(WorkItem);
        TransactionResult IWorkItemBizOf.Update() => DataProvider.Update(WorkItem);
        TransactionResult IWorkItemBizOf.Delete() => DataProvider.Delete<WorkItem>(WorkItem.Id);
    }
}
