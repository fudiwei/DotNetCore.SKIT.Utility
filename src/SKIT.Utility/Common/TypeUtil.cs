using System;
using System.Globalization;

namespace SKIT.Utility
{
    /// <summary>
    /// 类型工具类。
    /// </summary>
    public static class TypeUtil
    {
        /// <summary>
        /// 判断对象是否为 null 或 DBNull。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrDBNull(object obj)
        {
#if NETSTANDARD1_6
            return obj == null;
#else
            return (obj == null || Convert.IsDBNull(obj));
#endif
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? ToNullableInt32(object obj)
        {
            return IsNullOrDBNull(obj) ? null : new int?(Convert.ToInt32(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long? ToNullableInt64(object obj)
        {
            return IsNullOrDBNull(obj) ? null : new long?(Convert.ToInt64(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float? ToNullableSingle(object obj)
        {
            return IsNullOrDBNull(obj) ? null : new float?(Convert.ToSingle(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double? ToNullableDouble(object obj)
        {
            return IsNullOrDBNull(obj) ? null : new double?(Convert.ToDouble(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? ToNullableBoolean(object obj)
        {
            return IsNullOrDBNull(obj) ? null : new bool?(Convert.ToBoolean(obj));
        }

        /// <summary>
        /// 返回一个指定类型的对象，该对象的值等于指定的对象，支持可空类型和枚举类型。
        /// </summary>
        /// <param name="value">用于实现 System.IConvertible 接口的对象。</param>
        /// <param name="conversionType">要返回的对象的类型。</param>
        /// <returns>
        /// 一个类型为 conversionType 的对象，其值等效于 value。 
        /// 如果 value 的 Type 与 conversionType 相等，则为 value。 
        /// 如果 value 是 null，且conversionType 不是值类型，则为 null 引用。 
        /// </returns>
        public static object ChangeType(object value, Type conversionType)
        {
            return ChangeType(value, conversionType, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 返回一个指定类型的对象，该对象的值等于指定的对象，支持可空类型和枚举类型。
        /// </summary>
        /// <param name="value">用于实现 System.IConvertible 接口的对象。</param>
        /// <param name="conversionType">要返回的对象的类型。</param>
        /// <param name="provider">一个提供区域性特定的格式设置信息的对象。</param>
        /// <returns>
        /// 一个类型为 conversionType 的对象，其值等效于 value。 
        /// 如果 value 的 Type 与 conversionType 相等，则为 value。 
        /// 如果 value 是 null，且conversionType 不是值类型，则为 null 引用。 
        /// </returns>
        public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            Type nullableType = Nullable.GetUnderlyingType(conversionType);
            if (nullableType != null)
            {
                if (value == null)
                {
                    return null;
                }
                return Convert.ChangeType(value, nullableType, provider);
            }

#if NETSTANDARD1_6
            try
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            catch (ArgumentException) { }
#else
            if (typeof(Enum).IsAssignableFrom(conversionType))
            {
                return Enum.Parse(conversionType, value.ToString());
            }
#endif

            return Convert.ChangeType(value, conversionType, provider);
        }
    }
}
