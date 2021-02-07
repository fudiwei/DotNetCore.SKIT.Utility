using System;
using System.Security.Cryptography;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// 三重数据加密算法（Triple Data Encryption Algorithm）工具类。
    /// </summary>
    public static class TripleDesUtil
    {
        private const CipherMode DEFAULT_CIPHER_MODE = CipherMode.ECB;
        private const PaddingMode DEFAULT_PADDING_MODE = PaddingMode.PKCS7;

        /// <summary>
        /// 空初始化向量，即无偏移量。
        /// </summary>
        public readonly static byte[] EmptyIVBytes = new byte[0];

        /// <summary>
        /// 空初始化向量，即无偏移量。
        /// </summary>
        public const string EmptyIV = "";

        private static bool TryConvert2DESKeyTo3DESKey(byte[] keyBytes, out byte[] bKey)
        {
            if (keyBytes.Length == 16)
            {
                bKey = new byte[24];
                Buffer.BlockCopy(keyBytes, 0, bKey, 0, keyBytes.Length);
                Buffer.BlockCopy(keyBytes, 0, bKey, keyBytes.Length, 8);
                return true;
            }

            bKey = null;
            return false;
        }

        /// <summary>
        /// 3DES 加密。
        /// </summary>
        /// <param name="plainBytes">明文的字节数组。</param>
        /// <param name="keyBytes">密钥的字节数组（需 128 / 192 比特）。</param>
        /// <param name="ivBytes">向量的字节数组（需 128 比特）。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns>密文的字节数组。</returns>
        public static byte[] Encrypt(byte[] plainBytes, byte[] keyBytes, byte[] ivBytes, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(keyBytes, nameof(keyBytes));
            Guard.CheckArgumentNotNull(ivBytes, nameof(ivBytes));

            using (SymmetricAlgorithm des = TripleDES.Create())
            {
                if (cipherMode != CipherMode.ECB) des.IV = ivBytes;
                des.Key = TryConvert2DESKeyTo3DESKey(keyBytes, out byte[] bKey) ? bKey : keyBytes;
                des.Mode = cipherMode;
                des.Padding = paddingMode;

                using (ICryptoTransform transform = des.CreateEncryptor())
                {
                    return transform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }
        }

        /// <summary>
        /// 3DES 加密。
        /// </summary>
        /// <param name="plain">明文。</param>
        /// <param name="key">密钥。</param>
        /// <param name="iv">向量。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns>密文的 Base64 编码结果。</returns>
        public static string Encrypt(string plain, string key, string iv = EmptyIV, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            return Encrypt(plain, key, iv, Encoding.UTF8, cipherMode, paddingMode);
        }

        /// <summary>
        /// 3DES 加密。
        /// </summary>
        /// <param name="plain">明文。</param>
        /// <param name="key">密钥。</param>
        /// <param name="iv">向量。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns>密文的 Base64 编码结果。</returns>
        public static string Encrypt(string plain, string key, string iv, Encoding encoding, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            Guard.CheckArgumentNotNullOrEmpty(key, nameof(key));

            byte[] bytes = Encrypt(encoding.GetBytes(plain), encoding.GetBytes(key), encoding.GetBytes(iv), cipherMode, paddingMode);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 3DES 解密。
        /// </summary>
        /// <param name="cipherBytes">密文的字节数组。</param>
        /// <param name="keyBytes">密钥的字节数组（需 128 / 192 比特）。</param>
        /// <param name="ivBytes">向量的字节数组（需 128 比特）。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns>明文的字节数组。</returns>
        public static byte[] Decrypt(byte[] cipherBytes, byte[] keyBytes, byte[] ivBytes, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            Guard.CheckArgumentNotNull(cipherBytes, nameof(cipherBytes));
            Guard.CheckArgumentNotNull(keyBytes, nameof(keyBytes));
            Guard.CheckArgumentNotNull(ivBytes, nameof(ivBytes));

            using (SymmetricAlgorithm des = TripleDES.Create())
            {
                if (cipherMode != CipherMode.ECB) des.IV = ivBytes;
                des.Key = TryConvert2DESKeyTo3DESKey(keyBytes, out byte[] bKey) ? bKey : keyBytes;
                des.Mode = cipherMode;
                des.Padding = paddingMode;

                using (ICryptoTransform transform = des.CreateDecryptor())
                {
                    return transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                }
            }
        }

        /// <summary>
        /// 3DES 解密。
        /// </summary>
        /// <param name="cipher">密文的 Base64 编码结果。</param>
        /// <param name="key">密钥。</param>
        /// <param name="iv">向量。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns></returns>
        public static string Decrypt(string cipher, string key, string iv, Encoding encoding, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] bytes = Decrypt(Convert.FromBase64String(cipher), encoding.GetBytes(key), encoding.GetBytes(iv), cipherMode, paddingMode);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 3DES 解密。
        /// </summary>
        /// <param name="cipher">密文的 Base64 编码结果。</param>
        /// <param name="key">密钥。</param>
        /// <param name="iv">向量。</param>
        /// <param name="cipherMode">加密模式（默认 <see cref="CipherMode.ECB" />）。</param>
        /// <param name="paddingMode">填充模式（默认 <see cref="PaddingMode.PKCS7" />）。</param>
        /// <returns>明文。</returns>
        public static string Decrypt(string cipher, string key, string iv = EmptyIV, CipherMode cipherMode = DEFAULT_CIPHER_MODE, PaddingMode paddingMode = DEFAULT_PADDING_MODE)
        {
            return Decrypt(cipher, key, iv, Encoding.UTF8, cipherMode, paddingMode);
        }
    }
}
