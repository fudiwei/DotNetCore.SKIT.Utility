using STEP.Utility;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// 判断当前字符是否是全角字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDoubleByte(this char value)
        {
            return StringUtil.IsDoubleByte(value);
        }

        /// <summary>
        /// 将当前字符转为对应的全角字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToDoubleByte(this char value)
        {
            return StringUtil.ToDoubleByte(value);
        }

        /// <summary>
        /// 将当前字符转为对应的半角字符。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char ToSingleByte(this char value)
        {
            return StringUtil.ToSingleByte(value);
        }
    }
}
