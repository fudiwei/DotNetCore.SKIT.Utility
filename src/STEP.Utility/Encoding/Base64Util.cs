using System;
using System.Text;

namespace STEP.Utility
{
    /// <summary>
    /// Base64 编码工具类。
    /// </summary>
    public static class Base64Util
    {
        /// <summary>
        /// 将字节数组进行 Base64 编码。
        /// </summary>
        /// <param name="bytes">待编码数据的字节数组。</param>
        /// <param name="urlSafe">是否符合 RFC-4648 以适用于 URL 安全（默认值 false）。</param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes, bool urlSafe = false)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            string result = Convert.ToBase64String(bytes);
            if (urlSafe)
            {
                result = result.TrimEnd('=')
                    .Replace('+', '-').Replace('/', '_');
            }

            return result;
        }

        /// <summary>
        /// 将字符串进行 Base64 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static byte[] FromBase64String(string encodedText)
        {
            if (string.IsNullOrEmpty(encodedText))
                return new byte[0];

            string incoming = encodedText.Replace('_', '/').Replace('-', '+');
            switch (encodedText.Length % 4)
            {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }

            return Convert.FromBase64String(incoming);
        }

        /// <summary>
        /// 将字符串进行基于指定字符集的 Base64 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <param name="urlSafe">是否符合 RFC-4648 以适用于 URL 安全（默认值 false）。</param>
        /// <returns></returns>
        public static string Encode(string text, Encoding encoding, bool urlSafe = false)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return ToBase64String(encoding.GetBytes(text), urlSafe);
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base64 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <param name="urlSafe">是否符合 RFC-4648 以适用于 URL 安全（默认值 false）。</param>
        /// <returns></returns>
        public static string Encode(string text, bool urlSafe = false)
        {
            return Encode(text, Encoding.UTF8, urlSafe);
        }

        /// <summary>
        /// 将字符串进行指定字符集的 Base64 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Decode(string encodedText, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(encodedText))
                return string.Empty;

            return encoding.GetString(FromBase64String(encodedText));
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base64 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
            return Decode(encodedText, Encoding.UTF8);
        }
    }
}
