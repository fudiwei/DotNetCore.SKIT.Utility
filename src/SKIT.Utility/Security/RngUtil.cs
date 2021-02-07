using System;
using System.Security.Cryptography;

namespace SKIT.Utility
{
    /// <summary>
    /// 随机数生成器工具类。
    /// </summary>
    public static class RngUtil
    {
        private static readonly RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();

        /// <summary>
        /// 产生一个非负随机数。
        /// </summary>
        /// <returns></returns>
        public static long GetRandom()
        {
            byte[] randomBytes = new byte[8];
            randomGenerator.GetBytes(randomBytes);
            long random = BitConverter.ToInt64(randomBytes, 0);
            return Math.Abs(random);
        }

        /// <summary>
        /// 产生一个最大值为 max （可以取到 max）的非负随机数。
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long GetRandom(long max)
        {
            Guard.CheckArgumentInRange(max, nameof(max), (i) => i > 0);

            long random = GetRandom() % (max + 1);
            return Math.Abs(random);
        }

        /// <summary>
        /// 产生一个介于 min 和 max （可以取到 max）之间的非负随机数。
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static long GetRandom(long min, long max)
        {
            Guard.CheckArgumentInRange(max, nameof(max), (i) => min > 0 && max > 0 && min <= max);

            return GetRandom(max - min) + min;
        }
    }
}