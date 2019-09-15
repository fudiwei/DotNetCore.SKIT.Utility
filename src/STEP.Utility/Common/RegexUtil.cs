using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace STEP.Utility
{
    /// <summary>
    /// 正则表达式工具类。
    /// </summary>
    public static class RegexUtil
    {
        /// <summary>
        /// 判断字符是否为纯数字。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 判断字符是否为无符号数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsignNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 判断字符是否为整数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// 判断字符是否为无符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsignInteger(string value)
        {
            return Regex.IsMatch(value, @"^[+]?\d*$");
        }

        /// <summary>
        /// 判断字符是否为QQ号码。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsQQ(string value)
        {
            return Regex.IsMatch(value, @"^\d{5,10}$");
        }

        /// <summary>
        /// 判断字符是否为（中国大陆）电话号码。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPRCTelephone(string value)
        {
            return Regex.IsMatch(value, @"^(\d{3,4}-)?\d{6,8}$|^\d{9,12}$");
        }

        /// <summary>
        /// 判断字符是否为（中国大陆）手机号码。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPRCMobilephone(string value)
        {
            return Regex.IsMatch(value, @"^[1][2-9]\d{9}$");
        }

        /// <summary>
        /// 判断字符是否为电子邮箱。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmail(string value)
        {
            //return Regex.IsMatch(value, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return Regex.IsMatch(value, @"^[0-9a-zA-Z][_.0-9a-zA-Z-]{0,31}@([0-9a-zA-Z][0-9a-zA-Z-]{0,30}[0-9a-zA-Z]\.){1,4}[a-zA-Z]{2,4}$");
        }

        /// <summary>
        /// 判断字符是否为 IPv4 地址。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIPv4(string value)
        {
            return Regex.IsMatch(value, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }

        /// <summary>
        /// 判断字符是否为 IPv6 地址。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIPv6(string value)
        {
            return Regex.IsMatch(value, @"^((([0-9A-Fa-f]{1,4}:){7}[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){1,7}:)|(([0-9A-Fa-f]{1,4}:){6}:[0-9A-Fa-f]{1,4})|(([0-9A-Fa-f]{1,4}:){5}(:[0-9A-Fa-f]{1,4}){1,2})|(([0-9A-Fa-f]{1,4}:){4}(:[0-9A-Fa-f]{1,4}){1,3})|(([0-9A-Fa-f]{1,4}:){3}(:[0-9A-Fa-f]{1,4}){1,4})|(([0-9A-Fa-f]{1,4}:){2}(:[0-9A-Fa-f]{1,4}){1,5})|([0-9A-Fa-f]{1,4}:(:[0-9A-Fa-f]{1,4}){1,6})|(:(:[0-9A-Fa-f]{1,4}){1,7})|(([0-9A-Fa-f]{1,4}:){6}(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|(([0-9A-Fa-f]{1,4}:){5}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|(([0-9A-Fa-f]{1,4}:){4}(:[0-9A-Fa-f]{1,4}){0,1}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|(([0-9A-Fa-f]{1,4}:){3}(:[0-9A-Fa-f]{1,4}){0,2}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|(([0-9A-Fa-f]{1,4}:){2}(:[0-9A-Fa-f]{1,4}){0,3}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|([0-9A-Fa-f]{1,4}:(:[0-9A-Fa-f]{1,4}){0,4}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3})|(:(:[0-9A-Fa-f]{1,4}){0,5}:(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])(\\.(\\d|[1-9]\\d|1\\d{2}|2[0-4]\\d|25[0-5])){3}))$");
        }

        /// <summary>
        /// 过滤所有 HTML 标签。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            string output = Regex.Replace(input, "<[^>]*>", "");
            output = Regex.Replace(output, "&[^;]+;", "");
            return output;
        }

        /// <summary>
        /// 过滤所有换行。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterLineBreaks(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input
                .Replace("\r", "")
                .Replace("\n", "");
        }

        /// <summary>
        /// 过滤所有空白符。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterWhiteSpaces(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input
                .Replace(" ", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\t", "");
        }

        /// <summary>
        /// 过滤所有 Emoji 字符。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilterEmoji(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            string output = Regex.Replace(input, @"\p{Cs}", "");
            return output;
        }

        /// <summary>
        /// 返回字符串满足正则表达式的子串。
        /// </summary>
        /// <param name="regexPattern">正则表达式。</param>
        /// <param name="input">指定输入。</param>
        /// <returns></returns>
        public static string Match(string regexPattern, string input)
        {
            return Match(new Regex(regexPattern), input);
        }

        /// <summary>
        /// 返回字符串满足正则表达式的子串。
        /// </summary>
        /// <param name="regex">正则表达式。</param>
        /// <param name="input">指定输入。</param>
        /// <returns></returns>
        public static string Match(Regex regex, string input)
        {
            Guard.CheckArgumentNotNull(regex, nameof(regex));

            if (string.IsNullOrEmpty(input)) return null;

            var match = regex.Match(input);
            return match.Success ? match.Groups[1].Value : null;
        }
    }
}
