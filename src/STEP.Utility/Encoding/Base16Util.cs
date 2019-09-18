using System;
using System.Linq;
using System.Text;

namespace STEP.Utility
{
    /// <summary>
    /// Base16 编码工具类。
    /// </summary>
    public static class Base16Util
    {
        /// <summary>
        /// 将字节数组转换为一个 Base16 字符串。
        /// </summary>
        /// <param name="bytes">待编码数据的字节数组。</param>
        /// <returns></returns>
        public static string ToBase16String(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            StringBuilder builder = new StringBuilder(bytes.Length * 2);

            for (int i = 0, len = bytes.Length; i < len; i++)
            {
                builder.Append((bytes[i] & 0xff).ToString("X").PadLeft(2, '0'));
            }

            return builder.ToString();
        }

        /// <summary>
        /// 将 Base16 字符串转换为一个字节数组。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static byte[] FromBase16String(string encodedText)
        {
            if (string.IsNullOrEmpty(encodedText))
                return new byte[0];

            if (encodedText.Length % 2 != 0)
                throw new FormatException("Not a valid Base16 encoded string.");

            byte[] buffer = new byte[encodedText.Length / 2];
            for (int i = 0, len = buffer.Length; i < len; i++)
            {
                buffer[i] = Convert.ToByte(encodedText.Substring(2 * i, 2), 16);
            }
            return buffer;
        }

        /// <summary>
        /// 将字符串进行基于指定字符集的 Base16 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Encode(string text, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return ToBase16String(encoding.GetBytes(text));
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base16 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <returns></returns>
        public static string Encode(string text)
        {
            return Encode(text, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串进行指定字符集的 Base16 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Decode(string encodedText, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            if (string.IsNullOrEmpty(encodedText))
                return string.Empty;

            return encoding.GetString(FromBase16String(encodedText));
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 Base16 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
            return Decode(encodedText, Encoding.UTF8);
        }
    }
}
