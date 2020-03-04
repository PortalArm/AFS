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
            var a = dbSet.Local;
            var b = a.Where(predicate.Compile());
            if (b.FirstOrDefault() != null)
                return b.ToList();
            var c = dbSet.Where(predicate);
            var d = c.ToList();
            return d;
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
            //return local.Any()
            //    ? local
            //    : (dbSet.Where(predicate)).ToArray();
        }
    }
}
