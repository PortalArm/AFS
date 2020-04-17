using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Extensions
{
    public static class EFExtensions
    {
        public static IEnumerable<T> FindPredicate<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate) where T : class
        {
            //если использовать local, то баг: если бы я подгрузил один файл в какой-либо папке с другими файлами через короткую
            //ссылку, то в обычном списке был бы только один файл...
            //использовать это, либо подправить кейсы использования, видимо где-то отваливается трекинг
            return dbSet.Where(predicate).ToList();
            
            //var local = dbSet.Local.Where(predicate.Compile()).ToList();
            //return local.Any()
            //    ? local
            //    : (dbSet.Where(predicate)).ToList();
            
            
            
            //var b = a.Where(predicate.Compile());

            //var a = dbSet.Local.ToList().Where(predicate.Compile());
            //if (a.FirstOrDefault() != null)
            //    return a.ToList();
            //var c = dbSet.Where(predicate);
            //var d = c.ToList();
            //return d;

            //var c = b.ToList();
            //return c;
            //IEnumerable<T> local = null;
            //if(dbSet.Local != null)
            //    local = dbSet.Local.Where(predicate.Compile());
            ////return local.Any()
            ////    ? local
            ////    : dbSet.Where(predicate).ToArray();
            //if (local != null)
            //    return local;
            //var fromDb = dbSet.Where(predicate);
            //if (fromDb.Any())
            //    return fromDb.ToArray();
            //return null;


        }
    }
}
