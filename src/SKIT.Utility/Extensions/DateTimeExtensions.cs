using SKIT.Utility;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 获取当前时间的 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime dateTime, TimeSpan offset)
        {
            return DateTimeUtil.ToUnixTimeSeconds(dateTime, offset);
        }

        /// <summary>
        /// 获取当前时间的 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            return DateTimeUtil.ToUnixTimeSeconds(dateTime);
        }

        /// <summary>
        /// 获取当前时间的 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime, TimeSpan offset)
        {
            return DateTimeUtil.ToUnixTimeMilliseconds(dateTime, offset);
        }

        /// <summary>
        /// 获取当前时间的 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return DateTimeUtil.ToUnixTimeMilliseconds(dateTime);
        }

        /// <summary>
        /// 设置当前时间为指定的 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeSeconds(this DateTime dateTime, long timestamp)
        {
            return DateTimeUtil.FromUnixTimeSeconds(timestamp);
        }

        /// <summary>
        /// 设置当前时间为指定的 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimeMilliseconds(this DateTime dateTime, long timestamp)
        {
            return DateTimeUtil.FromUnixTimeMilliseconds(timestamp);
        }
    }
}
