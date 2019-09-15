using STEP.Utility;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 返回字符串左起的若干个字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            return StringUtil.Left(value, length);
        }

        /// <summary>
        /// 返回字符串右起的若干个字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            return StringUtil.Right(value, length);
        }

        /// <summary>
        /// 返回字符串的反转结果。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Reverse(this string value)
        {
            return StringUtil.Reverse(value);
        }

        /// <summary>
        /// 移除字符串两侧的指定字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static string Trim(this string value, string trimString)
        {
            while (true)
            {
                string result = TrimEnd(TrimStart(value, trimString), trimString);
                if (value == result) break;

                value = result;
            }
            return value;
        }

        /// <summary>
        /// 移除字符串左侧的指定字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static string TrimStart(this string value, string trimString)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(trimString))
                return value;

            int len = trimString.Length;
            while (value.StartsWith(trimString))
            {
                value = value.Substring(len);
            }
            return value;
        }

        /// <summary>
        /// 移除字符串右侧的指定字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="trimString"></param>
        /// <returns></returns>
        public static string TrimEnd(this string value, string trimString)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(trimString))
                return value;

            int len = trimString.Length;
            while (value.EndsWith(trimString))
            {
                value = value.Substring(0, value.Length - len);
            }
            return value;
        }
    }
}
