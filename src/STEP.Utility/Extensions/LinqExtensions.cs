using System;
using System.Collections.Generic;
using STEP.Utility;

namespace System.Linq
{
    /// <summary>
    /// 
    /// </summary>
    public static class LinqExtensions
    {
        private sealed class DynamicEqualityComparer<T> : IEqualityComparer<T>
            where T : class
        {
            private readonly Func<T, T, bool> _func;

            public DynamicEqualityComparer(Func<T, T, bool> func)
            {
                _func = func;
            }

            public bool Equals(T x, T y) => _func(x, y);

            public int GetHashCode(T obj) => 0;
        }

        /// <summary>
        /// 通过使用指定的相等比较器对值进行比较，返回序列中的非重复元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要从中移除重复元素的序列。</param>
        /// <param name="comparer">指定的相等比较器。</param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> comparer)
            where TSource : class
        {
            if (comparer == null)
                return source.Distinct();

            return source.Distinct(new DynamicEqualityComparer<TSource>(comparer));
        }

        /// <summary>
        /// 通过使用指定的属性对值进行比较，返回序列中的非重复元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">要从中移除重复元素的序列。</param>
        /// <param name="keySelector">指定的属性。</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (keySelector == null)
                Guard.CheckArgumentNotNull(keySelector, nameof(keySelector));

            return source.GroupBy(keySelector).Select(e => e.First());
        }

        /// <summary>
        /// 返回序列中的随机一个元素；如果不存在任一元素，则返回默认值。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <returns></returns>
        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return RandomOrDefault(source, (e) => true);
        }

        /// <summary>
        /// 返回序列中满足条件的随机一个元素；如果不存在任一元素，则返回默认值。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数。</param>
        /// <returns></returns>
        public static TSource RandomOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).OrderBy(e => Guid.NewGuid()).FirstOrDefault();
        }

        /// <summary>
        /// 返回序列中的随机一个元素；如果不存在任一元素，则抛出 <see cref="InvalidOperationException"/> 异常。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <returns></returns>
        public static TSource Random<TSource>(this IEnumerable<TSource> source)
        {
            return Random(source, (e) => true);
        }

        /// <summary>
        /// 返回序列中满足条件的随机一个元素；如果不存在任一元素，则抛出 <see cref="InvalidOperationException"/> 异常。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <param name="predicate">用于测试每个元素是否满足条件的函数。</param>
        /// <returns></returns>
        public static TSource Random<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).OrderBy(e => Guid.NewGuid()).First();
        }
    }
}
