using System;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// 百分号编码（Percent Encoding / URL Encoding）工具类。
    /// </summary>
    public static class PercentEncodingUtil
    {
        /// <summary>
        /// 将字符串进行百分号编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <returns></returns>
        public static string Encode(string text)
        {
            /*
             * 解决编码后空格变加号问题。
             * REF: https://my.oschina.net/joymufeng/blog/620205
             */

            const int maxLength = 32766;

            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length <= maxLength)
                return Uri.EscapeDataString(text);

            StringBuilder builder = new StringBuilder(text.Length * 2);
            int index = 0;

            while (index < text.Length)
            {
                int len = Math.Min(text.Length - index, maxLength);
                string subString = text.Substring(index, len);

                builder.Append(Uri.EscapeDataString(subString));
                index += subString.Length;
            }

            return builder.ToString();
        }

        /// <summary>
        /// 将字符串进行百分号解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
#if NETSTANDARD1_6
            return Uri.UnescapeDataString(encodedText);
#else
            return System.Web.HttpUtility.UrlDecode(encodedText);
#endif
        }
    }
}
