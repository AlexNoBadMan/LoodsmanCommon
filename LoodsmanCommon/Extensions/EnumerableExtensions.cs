using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LoodsmanCommon.Extensions
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> source) => new ReadOnlyCollection<T>(source.ToArray());
    }
}
