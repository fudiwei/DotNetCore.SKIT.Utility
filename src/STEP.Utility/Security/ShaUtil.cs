using System;
using System.Security.Cryptography;
using System.Text;

namespace STEP.Utility
{
    /// <summary>
    /// 安全散列算法（Secure Hash Algorithm）工具类，包括 SHA1 / SHA256 / SHA384 / SHA512 / MD5 等常见算法。
    /// </summary>
    public static class ShaUtil
    {
        #region SHA-1
        /// <summary>
        /// HMAC-SHA1 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <param name="secretBytes">密钥的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] HMacSHA1Compute(byte[] plainBytes, byte[] secretBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(secretBytes, nameof(secretBytes));

            using (HMAC hmac = new HMACSHA1())
            {
                hmac.Key = secretBytes;
                return hmac.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// HMAC-SHA1 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA1Compute(string plain, string secret)
        {
            return HMacSHA1Compute(plain, secret, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-SHA1 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA1Compute(string plain, string secret, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = HMacSHA1Compute(encoding.GetBytes(plain), encoding.GetBytes(secret));
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// SHA1 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] SHA1Compute(byte[] plainBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));

            using (SHA1 sha = SHA1.Create())
            {
                return sha.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// SHA1 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA1Compute(string plain)
        {
            return SHA1Compute(plain, Encoding.UTF8);
        }

        /// <summary>
        /// SHA1 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA1Compute(string plain, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = SHA1Compute(encoding.GetBytes(plain));
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion

        #region SHA-256
        /// <summary>
        /// HMAC-SHA256 哈希。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <param name="secretBytes">密钥的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] HMacSHA256Compute(byte[] plainBytes, byte[] secretBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(secretBytes, nameof(secretBytes));

            using (HMAC hmac = new HMACSHA256())
            {
                hmac.Key = secretBytes;
                return hmac.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// HMAC-SHA256 哈希。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA256Compute(string plain, string secret)
        {
            return HMacSHA256Compute(plain, secret, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-SHA256 哈希。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA256Compute(string plain, string secret, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = HMacSHA256Compute(encoding.GetBytes(plain), encoding.GetBytes(secret));
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// SHA256 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] SHA256Compute(byte[] plainBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));

            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// SHA256 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA256Compute(string plain)
        {
            return SHA256Compute(plain, Encoding.UTF8);
        }

        /// <summary>
        /// SHA256 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA256Compute(string plain, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = SHA256Compute(encoding.GetBytes(plain));
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion

        #region SHA-384
        /// <summary>
        /// HMAC-SHA384 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <param name="secretBytes">密钥的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] HMacSHA384Compute(byte[] plainBytes, byte[] secretBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(secretBytes, nameof(secretBytes));

            using (HMAC hmac = new HMACSHA384())
            {
                hmac.Key = secretBytes;
                return hmac.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// HMAC-SHA384 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA384Compute(string plain, string secret)
        {
            return HMacSHA384Compute(plain, secret, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-SHA384 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA384Compute(string plain, string secret, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = HMacSHA384Compute(encoding.GetBytes(plain), encoding.GetBytes(secret));
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// SHA384 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] SHA384Compute(byte[] plainBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));

            using (SHA384 sha = SHA384.Create())
            {
                return sha.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// SHA384 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA384Compute(string plain)
        {
            return SHA384Compute(plain, Encoding.UTF8);
        }

        /// <summary>
        /// SHA384 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA384Compute(string plain, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = SHA384Compute(encoding.GetBytes(plain));
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion

        #region SHA-512
        /// <summary>
        /// HMAC-SHA512 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <param name="secretBytes">密钥的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] HMacSHA512Compute(byte[] plainBytes, byte[] secretBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(secretBytes, nameof(secretBytes));

            using (HMAC hmac = new HMACSHA512())
            {
                hmac.Key = secretBytes;
                return hmac.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// HMAC-SHA512 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA512Compute(string plain, string secret)
        {
            return HMacSHA512Compute(plain, secret, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-SHA512 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacSHA512Compute(string plain, string secret, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = HMacSHA512Compute(encoding.GetBytes(plain), encoding.GetBytes(secret));
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// SHA512 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] SHA512Compute(byte[] plainBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));

            using (SHA512 sha = SHA512.Create())
            {
                return sha.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// SHA512 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA512Compute(string plain)
        {
            return SHA512Compute(plain, Encoding.UTF8);
        }

        /// <summary>
        /// SHA512 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string SHA512Compute(string plain, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = SHA512Compute(encoding.GetBytes(plain));
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion

        #region MD5
        /// <summary>
        /// HMAC-MD5 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <param name="secretBytes">密钥的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] HMacMD5Compute(byte[] plainBytes, byte[] secretBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(secretBytes, nameof(secretBytes));

            using (HMAC hmac = new HMACMD5())
            {
                hmac.Key = secretBytes;
                return hmac.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// HMAC-MD5 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacMD5Compute(string plain, string secret)
        {
            return HMacMD5Compute(plain, secret, Encoding.UTF8);
        }

        /// <summary>
        /// HMAC-MD5 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="secret">密钥。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string HMacMD5Compute(string plain, string secret, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = HMacMD5Compute(encoding.GetBytes(plain), encoding.GetBytes(secret));
            return BitConverter.ToString(output).Replace("-", "");
        }

        /// <summary>
        /// MD5 哈希计算。
        /// </summary>
        /// <param name="plainBytes">待哈希的字节数组。</param>
        /// <returns>信息摘要的字节数组。</returns>
        public static byte[] MD5Compute(byte[] plainBytes)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));

            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(plainBytes);
            }
        }

        /// <summary>
        /// MD5 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <returns>信息摘要。</returns>
        public static string MD5Compute(string plain)
        {
            return MD5Compute(plain, Encoding.UTF8);
        }

        /// <summary>
        /// MD5 哈希计算。
        /// </summary>
        /// <param name="plain">待哈希的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns>信息摘要。</returns>
        public static string MD5Compute(string plain, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] output = MD5Compute(encoding.GetBytes(plain));
            return BitConverter.ToString(output).Replace("-", "");
        }
        #endregion
    }
}
