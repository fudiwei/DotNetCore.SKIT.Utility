using System;
using System.Linq;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// Base32 编码工具类。
    /// </summary>
    public static class Base32Util
    {
        private const string Base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        /// <summary>
        /// 将字节数组转换为一个 Base32 字符串。
        /// </summary>
        /// <param name="bytes">待编码数据的字节数组。</param>
        /// <returns></returns>
        public static string ToBase32String(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            string bits = bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).Aggregate((a, b) => a + b).PadRight((int)(Math.Ceiling((bytes.Length * 8) / 5d) * 5), '0');
            string result = Enumerable.Range(0, bits.Length / 5)
                .Select(i => Base32Chars.Substring(Convert.ToInt32(bits.Substring(i * 5, 5), 2), 1))
                .Aggregate((a, b) => a + b);
            result = result.PadRight((int)(Math.Ceiling(result.Length / 8d) * 8), '=');

            return result;
        }

        /// <summary>
        /// 将 Base32 字符串转换为一个字节数组。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static byte[] FromBase32String(string encodedText)
        {
            if (string.IsNullOrEmpty(encodedText))
                return new byte[0];

            string bits = encodedText.TrimEnd('=').ToUpper().ToCharArray()
                .Select(c => Convert.ToString(Base32Chars.IndexOf(c), 2).PadLeft(5, '0'))
                .Aggregate((a, b) => a + b);
            return Enumerable.Range(0, bits.Length / 8)
                .Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2))
                .ToArray();
        }

        /// <summary>
        /// 将字符串进行基于指定字符集的 Base32 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Encode(string text, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return ToBase32String(encoding.GetBytes(text));
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base32 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <returns></returns>
        public static string Encode(string text)
        {
            return Encode(text, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串进行指定字符集的 Base32 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Decode(string encodedText, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(encodedText))
                return string.Empty;

            return encoding.GetString(FromBase32String(encodedText));
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base32 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
            return Decode(encodedText, Encoding.UTF8);
        }
    }
}
