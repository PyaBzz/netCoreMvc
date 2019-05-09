﻿using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App.Consuming
{
    public interface IDataProvider
    {
        T Get<T>(Guid id) where T : class, IThing;
        T Get<T>(Func<T, bool> func) where T : class, IThing;

        List<T> GetList<T>() where T : class, IThing;
        List<T> GetList<T>(Func<T, bool> func) where T : class, IThing;
        List<T> GetListIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;
        List<T> GetListIncluding<T>(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties) where T : class, IThing;

        //Lesson: Why don't we say: TransactionResult Add(Thing obj)?
        // Because then in order to use it with any derived class off Thing,
        // we'd have to cast the subclass to Thing or use "as List<Thing>"
        TransactionResult Add<T>(T obj) where T : class, IThing;
        TransactionResult Update<T>(T obj) where T : class, IThing;
        TransactionResult Delete<T>(Guid id) where T : class, IThing;
    }
}