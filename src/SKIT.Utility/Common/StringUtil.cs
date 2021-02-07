using System;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// 字符串工具类。
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 判断字符是否是全角字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDoubleByte(char value)
        {
            return (value.ToString().Length != Encoding.UTF8.GetByteCount(value.ToString()));
        }

        /// <summary>
        /// 判断字符是否是全角字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDoubleByte(string value)
        {
            bool result = true;

            char[] c = value.ToCharArray();
            for (int i = 0, len = c.Length; i < len; i++)
            {
                if (!IsDoubleByte(value.ToCharArray()[i]))
                {
                    // 有一个字符不是全角，整体就不是全角。
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 转成半角字符。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static char ToDoubleByte(char input)
        {
            if (input == 32)
            {
                return (char)12288;
            }
            else if (input < 127)
            {
                return (char)(input + 65248);
            }

            return input;
        }

        /// <summary>
        /// 转成半角字符串。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDoubleByte(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0, len = c.Length; i < len; i++)
            {
                c[i] = ToDoubleByte(c[i]);
            }
            return new string(c);
        }

        /// <summary>
        /// 转成全角字符。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static char ToSingleByte(char input)
        {
            if (input == 12288)
            {
                return (char)32;
            }
            else if (input > 65280 && input < 65375)
            {
                return (char)(input - 65248);
            }

            return input;
        }

        /// <summary>
        /// 转成全角字符串。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSingleByte(string input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = ToSingleByte(c[i]);
            }
            return new string(c);
        }

        /// <summary>
        /// 获取字符串的字符长度（全角字符占2，半角字符占1）。
        /// </summary>
        /// <param name="value">要计算长度的字符串。</param>
        /// <returns></returns>
        public static int GetByteLength(string value)
        {
            if (value == null) return 0;

            int i = 0;
            foreach (char c in value)
            {
                if (IsDoubleByte(c)) i++;
                i++;
            }

            return i;
        }

        /// <summary>
        /// 截取固定长度字符串。
        /// </summary>
        /// <param name="value">要截取的字符串。</param>
        /// <param name="startIndex">起始位置（从零开始）。</param>
        /// <param name="length">固定长度。</param>
        /// <returns></returns>
        public static string Substring(string value, int startIndex, int length)
        {
            return (value.Length - startIndex < length) ? value.Substring(startIndex) : value.Substring(startIndex, length);
        }

        /// <summary>
        /// 返回字符串左起的若干个字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(string value, int length)
        {
            return value.Substring(0, length > value.Length ? value.Length : length);
        }

        /// <summary>
        /// 返回字符串右起的若干个字符。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(string value, int length)
        {
            return value.Substring(length > value.Length ? 0 : value.Length - length);
        }

        /// <summary>
        /// 返回字符串的反转结果。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Reverse(string value)
        {
            if (value == null) return null;

            char[] chrs = value.ToCharArray();
            Array.Reverse(chrs);
            return new string(chrs);
        }
    }
}
