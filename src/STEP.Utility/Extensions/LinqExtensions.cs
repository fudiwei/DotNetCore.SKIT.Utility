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
        /// 返回一个 <see cref="System.Boolean"/> 值，该值指示序列是否至少包含指定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要测试的序列。</param>
        /// <param name="minCount">最少应有的数量。</param>
        /// <returns></returns>
        public static bool AtLeast<TSource>(this IEnumerable<TSource> source, int minCount)
        {
            Guard.CheckArgumentNotNull(source, nameof(source));

            return source.Count() >= minCount;
        }

        /// <summary>
        /// 返回一个 <see cref="System.Boolean"/> 值，该值指示序列是否至少包含指定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要测试的序列。</param>
        /// <param name="minCount">最少应有的数量。</param>
        /// <returns></returns>
        public static bool AtLeast<TSource>(this IEnumerable<TSource> source, long minCount)
            where TSource : class
        {
            Guard.CheckArgumentNotNull(source, nameof(source));

            return source.LongCount() >= minCount;
        }

        /// <summary>
        /// 返回一个 <see cref="System.Boolean"/> 值，该值指示序列是否最多包含指定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要测试的序列。</param>
        /// <param name="maxCount">最多应有的数量。</param>
        /// <returns></returns>
        public static bool AtMost<TSource>(this IEnumerable<TSource> source, int maxCount)
        {
            Guard.CheckArgumentNotNull(source, nameof(source));

            return source.Count() <= maxCount;
        }

        /// <summary>
        /// 返回一个 <see cref="System.Boolean"/> 值，该值指示序列是否最多包含指定数量的元素。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">要测试的序列。</param>
        /// <param name="maxCount">最多应有的数量。</param>
        /// <returns></returns>
        public static bool AtMost<TSource>(this IEnumerable<TSource> source, long maxCount)
        {
            Guard.CheckArgumentNotNull(source, nameof(source));

            return source.LongCount() <= maxCount;
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
            Guard.CheckArgumentNotNull(keySelector, nameof(keySelector));

            return source.GroupBy(keySelector).Select(e => e.First());
        }

        /// <summary>
        /// 调用序列的每个元素上的转换函数并返回最大结果值；如果序列为空，则返回默认值。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <param name="selector">用于测试每个元素是否满足条件的函数。</param>
        /// <returns></returns>
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null || !source.Any())
                return default;

            return source.Max(selector);
        }

        /// <summary>
        /// 调用序列的每个元素上的转换函数并返回最小结果值；如果序列为空，则返回默认值。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source">要从中返回元素的序列。</param>
        /// <param name="selector">用于测试每个元素是否满足条件的函数。</param>
        /// <returns></returns>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null || !source.Any())
                return default;

            return source.Min(selector);
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
