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
            var local = dbSet.Local.Where(predicate.Compile());
            return local.Any()
                ? local
                : dbSet.Where(predicate).ToArray();
        }
    }
}
