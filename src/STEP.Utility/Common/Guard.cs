using System;
using System.Linq.Expressions;

namespace STEP.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class Guard
    {
        const string EXMSG_OBJECT_IS_NULL = "The provided argument must not be null.";
        const string EXMSG_STRING_IS_NULL_OR_EMPTY = "The provided System.String argument must not be empty.";
        const string EXMSG_GUID_IS_NULL_OR_EMPTY = "The provided System.Guid argument must not be empty.";
        const string EXMSG_ARGUMENT_IS_OUT_OF_RANGE = "The provided argument is out of range.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        public static void CheckArgumentNotNull(object argumentValue)
        {
            if (argumentValue == null)
                throw new ArgumentException(EXMSG_OBJECT_IS_NULL);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void CheckArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        public static void CheckArgumentNotNullOrEmpty(string argumentValue)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException(EXMSG_STRING_IS_NULL_OR_EMPTY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void CheckArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException(EXMSG_STRING_IS_NULL_OR_EMPTY, argumentName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        public static void CheckArgumentNotNullOrEmpty(Guid? argumentValue)
        {
            if (argumentValue == null || argumentValue == Guid.Empty)
                throw new ArgumentException(EXMSG_GUID_IS_NULL_OR_EMPTY);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void CheckArgumentNotNullOrEmpty(Guid? argumentValue, string argumentName)
        {
            if (argumentValue == null || argumentValue == Guid.Empty)
                throw new ArgumentException(EXMSG_GUID_IS_NULL_OR_EMPTY, argumentName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="validator"></param>
        public static void CheckArgumentInRange(object argumentValue, Func<object, bool> validator)
        {
            CheckArgumentNotNull(argumentValue, nameof(validator));

            if (!validator.Invoke(argumentValue))
                throw new ArgumentOutOfRangeException(EXMSG_ARGUMENT_IS_OUT_OF_RANGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentValue"></param>
        /// <param name="validator"></param>
        public static void CheckArgumentInRange<T>(T argumentValue, Func<T, bool> validator)
        {
            CheckArgumentNotNull(argumentValue, nameof(validator));

            if (!validator.Invoke(argumentValue))
                throw new ArgumentOutOfRangeException(EXMSG_ARGUMENT_IS_OUT_OF_RANGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        /// <param name="validator"></param>
        public static void CheckArgumentInRange(object argumentValue, string argumentName, Func<object, bool> validator)
        {
            CheckArgumentNotNull(validator, nameof(validator));

            if (!validator.Invoke(argumentValue))
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, EXMSG_ARGUMENT_IS_OUT_OF_RANGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        /// <param name="validator"></param>
        public static void CheckArgumentInRange<T>(T argumentValue, string argumentName, Func<T, bool> validator)
        {
            CheckArgumentNotNull(validator, nameof(validator));

            if (!validator.Invoke(argumentValue))
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, EXMSG_ARGUMENT_IS_OUT_OF_RANGE);
        }
    }
}
