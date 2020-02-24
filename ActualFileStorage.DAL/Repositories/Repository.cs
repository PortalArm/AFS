using ActualFileStorage.DAL.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        protected IAdapter _adapter;
        protected IAdapter Adapter { get => _adapter.LoadType(_type); }
        protected Type _type;
        public Repository(IAdapter adapter)
        {
            _type = typeof(T);
            _adapter = adapter;//.LoadType<T>();
        }

        public void Add(T obj) => Adapter.Add(obj);

        public IEnumerable<T> GetAll() => Adapter.FindAll().Cast<T>();
        public T GetById(int id) => Adapter.Find(id) as T;
        
        //??
        //public IEnumerable<T> GetByPredicate(Func<T, bool> pred) { throw new NotImplementedException(); }// => _adapter.

        public void Remove(T obj) => Adapter.Remove(obj);
        public void SaveChanges() => Adapter.SaveChanges();
        public void Dispose() => Adapter.Dispose();

        //public void ChangeType<TOut>()
        //{
        //    ChangeType(typeof(TOut));
        //}
        //public void ChangeType(Type tout)
        //{
        //    _type = tout;
        //}

        //public void GG()
        //{
        //    throw new NotImplementedException();
        //}
    }
    //class Repository<T> : IRepository<T> where T : class
    //{
    //    protected DbSet<T> _objects;
    //    public Repository(DbContext context)
    //    {
    //        _objects = context.Set<T>();
    //    }

    //    public void Add(T obj)
    //    {
    //        _objects.Add(obj);
    //    }

    //    public IEnumerable<T> GetAll()
    //    {
    //        return _objects.ToList();
    //    }

    //    public T GetById(int id)
    //    {
    //       return _objects.Find(id);
    //    }

    //    public IEnumerable<T> GetByPredicate(Func<T, bool> pred)
    //    {
    //        return _objects.Where(pred);
    //    }

    //    public void Remove(T obj)
    //    {
    //        _objects.Remove(obj);
    //    }
    //}
}
