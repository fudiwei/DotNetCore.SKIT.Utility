using System;
using System.Globalization;

namespace SKIT.Utility
{
    /// <summary>
    /// 日期时间工具类。
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 获取 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTime dateTime, TimeSpan offset)
        {
            return new DateTimeOffset(dateTime, offset).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取 Unix 时间戳（秒）。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        /// <summary>
        /// 获取 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <param name="offset">与协调世界时 (UTC) 之间的时间偏移量。</param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTime dateTime, TimeSpan offset)
        {
            return new DateTimeOffset(dateTime, offset).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 获取 Unix 时间戳（毫秒）。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Unix 时间戳（秒）转为 System.dateTime。
        /// </summary>
        /// <param name="timeStamp">Unix 时间戳（秒）。</param>
        /// <returns></returns>
        public static DateTime FromUnixTimeSeconds(long timeStamp)
        {
#if NETFRAMEWORK
            return DateTimeOffsetUtil.FromUnixTimeSeconds(timeStamp).LocalDateTime;
#else
            return DateTimeOffset.FromUnixTimeSeconds(timeStamp).LocalDateTime;
#endif
        }

        /// <summary>
        /// Unix 时间戳（毫秒）转为 System.dateTime。
        /// </summary>
        /// <param name="timeStamp">Unix 时间戳（毫秒）。</param>
        /// <returns></returns>
        public static DateTime FromUnixTimeMilliseconds(long timeStamp)
        {
#if NETFRAMEWORK
            return DateTimeOffsetUtil.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;
#else
            return DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;
#endif
        }

        /// <summary>
        /// 获得某个时间点距离当前时间的差值文本表达。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <returns></returns>
        public static string GetDiffText(DateTime dateTime)
        {
            return GetDiffText(dateTime, new CultureInfo("zh-CN"));
        }

        /// <summary>
        /// 获得某个时间点距离当前时间的差值文本表达。
        /// </summary>
        /// <param name="dateTime">指定时间。</param>
        /// <param name="cultureInfo">指定区域信息（默认值 zh-CN）。</param>
        /// <returns></returns>
        public static string GetDiffText(DateTime dateTime, CultureInfo cultureInfo)
        {
            Guard.CheckArgumentNotNull(cultureInfo, nameof(cultureInfo));

            DateTime now = (dateTime.Kind == DateTimeKind.Utc) ? DateTime.UtcNow : DateTime.Now;
            int minute = 60;
            int hour = minute * 60;
            int day = hour * 24;
            int week = day * 7;
            int month = day * 30;
            int year = day * 365;

            bool isCultureNotChinese = !"zh".Equals(cultureInfo.TwoLetterISOLanguageName);
                
            long diffValue = (long)(now - dateTime).TotalSeconds;
            if (diffValue > 0)
            {
                if (diffValue / year >= 1)
                {
                    long val = diffValue / year;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} year ago";
                    else if (isCultureNotChinese)
                        return $"{val} years ago";

                    return $"{val}年前";
                }
                else if (diffValue / month >= 1)
                {
                    long val = diffValue / month;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} month ago";
                    else if (isCultureNotChinese)
                        return $"{val} months ago";

                    return $"{val}个月前";
                }
                else if (diffValue / week >= 1)
                {
                    long val = diffValue / week;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} week ago";
                    else if (isCultureNotChinese)
                        return $"{val} weeks ago";

                    return $"{val}周前";
                }
                else if (diffValue / day >= 1)
                {
                    long val = diffValue / day;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} day ago";
                    else if (isCultureNotChinese)
                        return $"{val} days ago";

                    return $"{val}天前";
                }
                else if (diffValue / hour >= 1)
                {
                    long val = diffValue / hour;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} hour ago";
                    else if (isCultureNotChinese)
                        return $"{val} hours ago";

                    return $"{val}小时前";
                }
                else if (diffValue / minute >= 1)
                {
                    long val = diffValue / minute;
                    if (isCultureNotChinese && val > 1)
                        return $"{val} minute ago";
                    else if (isCultureNotChinese)
                        return $"{val} minutes ago";

                    return $"{val}分钟前";
                }
            }

            return isCultureNotChinese ? "just now" : "刚刚";
        }

        /// <summary>
        /// 获得指定日期所在周的第一天（星期日）。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfWeek(DateTime date)
        {
            int week = Convert.ToInt32(date.DayOfWeek);
            int dayDiff = (-1) * week;
            
            return date.AddDays(dayDiff).Date;
        }

        /// <summary>
        /// 获得指定日期所在周的最后一天（星期六）。
        /// </summary>
        /// <param name="date">指定日期。</param>
        /// <returns></returns>
        public static DateTime GetLastDayOfWeek(DateTime date)
        {
            int week = Convert.ToInt32(date.DayOfWeek);
            int dayDiff = (7 - week) - 1;
            
            return date.AddDays(dayDiff).Date;
        }
    }
}
