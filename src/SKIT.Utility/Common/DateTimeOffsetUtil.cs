using System;

namespace SKIT.Utility
{
    /// <summary>
    /// 日期时间工具类。
    /// </summary>
    public static class DateTimeOffsetUtil
    {
        /// <summary>
        /// 获取 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTimeOffset dateTimeOffset)
        {
            long num = dateTimeOffset.UtcDateTime.Ticks / 10000000L;
            return num - 62135596800L;
        }

        /// <summary>
        /// 获取 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTimeOffset dateTimeOffset)
        {
            long num = dateTimeOffset.UtcDateTime.Ticks / 10000L;
            return num - 62135596800000L;
        }

        /// <summary>
        /// Unix 时间戳（秒）转为 System.dateTimeOffset。
        /// </summary>
        /// <param name="timestamp">Unix 时间戳（秒）。</param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeSeconds(long timestamp)
        {
            if (timestamp < -62135596800L || timestamp > 253402300799L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp));
            }
            long ticks = timestamp * 10000000L + 621355968000000000L;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }

        /// <summary>
        /// Unix 时间戳（毫秒）转为 System.dateTimeOffset。
        /// </summary>
        /// <param name="timestamp">Unix 时间戳（毫秒）。</param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeMilliseconds(long timestamp)
        {
            if (timestamp < -62135596800000L || timestamp > 253402300799999L)
            {
                throw new ArgumentOutOfRangeException(nameof(timestamp));
            }
            long ticks = timestamp * 10000L + 621355968000000000L;
            return new DateTimeOffset(ticks, TimeSpan.Zero);
        }
    }
}
