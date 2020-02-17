using ActualFileStorage.DAL.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetByPredicate(Func<T,bool> pred);
        T GetById(int id);
        void Add(T obj);
        void Remove(T obj);

    }
}
