using ActualFileStorage.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Adapters
{

    public static class SimpleMapper
    {                                   //TODO: (Prop to string) -> string отображение
        private static Dictionary<Type, string> _dict = new Dictionary<Type, string>() {
            { typeof(User) , "Users" },
            { typeof(File) , "Files" },
            { typeof(Folder) , "Folders" },
        };
        public static string Get<T>() => Get(typeof(T));
        public static string Get(Type type)
        {
            if (!_dict.ContainsKey(type))
                throw new InvalidOperationException("No such table in a database.");

            return _dict[type];
        }
    }

    public interface IAdapter
    {
        IAdapter LoadType<T>();
        IAdapter LoadType(Type type); 
        object Add(object entity);
        IEnumerable AddRange(IEnumerable entities);
        object Find(params object[] keyValues);
        IEnumerable FindAll();
        object Remove(object entity);
        IEnumerable RemoveRange(IEnumerable entities);
    }
}
