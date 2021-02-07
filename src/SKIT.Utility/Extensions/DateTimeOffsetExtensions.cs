using SKIT.Utility;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
#if NETFRAMEWORK
        /// <summary>
        /// 获取当前时间的 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTimeOffset dateTimeOffset)
        {
            return DateTimeOffsetUtil.ToUnixTimeSeconds(dateTimeOffset);
        }

        /// <summary>
        /// 获取当前时间的 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTimeOffset dateTimeOffset)
        {
            return DateTimeOffsetUtil.ToUnixTimeMilliseconds(dateTimeOffset);
        }

        /// <summary>
        /// 设置当前时间为指定的 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeSeconds(this DateTimeOffset dateTimeOffset, long timestamp)
        {
            return DateTimeOffsetUtil.FromUnixTimeSeconds(timestamp);
        }

        /// <summary>
        /// 设置当前时间为指定的 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTimeOffset FromUnixTimeMilliseconds(this DateTimeOffset dateTimeOffset, long timestamp)
        {
            return DateTimeOffsetUtil.FromUnixTimeMilliseconds(timestamp);
        }
#endif
    }
}