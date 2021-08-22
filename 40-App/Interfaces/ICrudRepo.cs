using myCoreMvc.Domain;
using System;
using System.Collections.Generic;

namespace myCoreMvc.App.Interfaces
{
    public interface ICrudRepo<T>
    {
        T Save(T x);
        List<T> GetAll();
        T Get(Guid? id);
        T Get(string id);
        void Delete(Guid? id);
        void Delete(string id);
        void DeleteAll();
    }
}
