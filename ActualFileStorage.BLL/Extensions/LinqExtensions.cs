using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> TakeWhileInclude<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (T item in list)
            {
                yield return item;
                if (!predicate(item))
                    yield break;
            }
        }
    }
}
