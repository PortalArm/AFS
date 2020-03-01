using ActualFileStorage.DAL.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByPredicate(Expression<Func<T,bool>> pred);
        T GetById(int id);
        void Add(T obj);
        void Remove(T obj);
        void SaveChanges();
        //void ChangeType<TOut>();
        //void ChangeType(Type tout);

    }
}
