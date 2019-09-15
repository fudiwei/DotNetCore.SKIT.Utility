using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

#if NETSTANDARD
namespace STEP.Utility
{
    /// <summary>
    /// RSA 加密算法（RSA Algorithm）工具类。
    /// </summary>
    public static class RsaUtil
    {
        private static RSA CreateRsaProviderFromPrivateKey(byte[] privateKeyBits)
        {
            int GetIntegerSize(BinaryReader reader)
            {
                int count;

                byte bt = reader.ReadByte();
                if (bt != 0x02)
                    return 0;
                bt = reader.ReadByte();

                if (bt == 0x81)
                {
                    count = reader.ReadByte();
                }
                else if (bt == 0x82)
                {
                    byte highByte = reader.ReadByte();
                    byte lowByte = reader.ReadByte();
                    byte[] modint = { lowByte, highByte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;
                }

                while (reader.ReadByte() == 0x00)
                {
                    count -= 1;
                }

                reader.BaseStream.Seek(-1, SeekOrigin.Current);
                return count;
            }

            RSA rsa = RSA.Create();
            RSAParameters rsaParameters = new RSAParameters();

            using (BinaryReader reader = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort tmp = reader.ReadUInt16();

                if (tmp == 0x8130)
                    reader.ReadByte();
                else if (tmp == 0x8230)
                    reader.ReadInt16();
                else
                    throw new Exception("Unexpected value read reader.ReadUInt16()");

                tmp = reader.ReadUInt16();
                if (tmp != 0x0102)
                    throw new Exception("Unexpected version");

                bt = reader.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.Exponent = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.D = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.P = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.Q = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.DP = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.DQ = reader.ReadBytes(GetIntegerSize(reader));
                rsaParameters.InverseQ = reader.ReadBytes(GetIntegerSize(reader));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        private static RSA CreateRsaProviderFromPublicKey(byte[] x509Key)
        {
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            using (MemoryStream ms = new MemoryStream(x509Key))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                byte bt = 0;
                ushort tmp = reader.ReadUInt16();

                if (tmp == 0x8130)
                    reader.ReadByte();
                else if (tmp == 0x8230)
                    reader.ReadInt16();
                else
                    return null;

                seq = reader.ReadBytes(15);
                if (!Enumerable.SequenceEqual(seq, seqOid))
                    return null;

                tmp = reader.ReadUInt16();
                if (tmp == 0x8103)
                    reader.ReadByte();
                else if (tmp == 0x8203)
                    reader.ReadInt16();
                else
                    return null;

                bt = reader.ReadByte();
                if (bt != 0x00)
                    return null;

                tmp = reader.ReadUInt16();
                if (tmp == 0x8130)
                    reader.ReadByte();
                else if (tmp == 0x8230)
                    reader.ReadInt16();
                else
                    return null;

                tmp = reader.ReadUInt16();
                byte lowByte = 0x00;
                byte highByte = 0x00;

                if (tmp == 0x8102)
                {
                    lowByte = reader.ReadByte();
                }
                else if (tmp == 0x8202)
                {
                    highByte = reader.ReadByte();
                    lowByte = reader.ReadByte();
                }
                else
                {
                    return null;
                }

                byte[] modint = { lowByte, highByte, 0x00, 0x00 };
                int modsize = BitConverter.ToInt32(modint, 0);

                if (reader.PeekChar() == 0x00)
                {
                    reader.ReadByte();
                    modsize -= 1;
                }

                byte[] modulus = reader.ReadBytes(modsize);

                if (reader.ReadByte() != 0x02)
                    return null;

                int expBytes = (int)reader.ReadByte();
                byte[] exponent = reader.ReadBytes(expBytes);

                RSA rsa = RSA.Create();
                RSAParameters rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);

                return rsa;
            }
        }
        
        private static int GetMaxBlockSize(RSA provider, RSAEncryptionPadding padding)
        {
            int offset;

            if (padding.Mode == RSAEncryptionPaddingMode.Pkcs1)
                offset = 11;
            else if (RSAEncryptionPadding.OaepSHA1.OaepHashAlgorithm.Name.Equals(padding.OaepHashAlgorithm.Name))
                offset = 42;
            else if (RSAEncryptionPadding.OaepSHA256.OaepHashAlgorithm.Name.Equals(padding.OaepHashAlgorithm.Name))
                offset = 66;
            else if (RSAEncryptionPadding.OaepSHA384.OaepHashAlgorithm.Name.Equals(padding.OaepHashAlgorithm.Name))
                offset = 98;
            else if (RSAEncryptionPadding.OaepSHA512.OaepHashAlgorithm.Name.Equals(padding.OaepHashAlgorithm.Name))
                offset = 130;
            else
                throw new NotSupportedException("Unsupported encryption padding.");

            return provider.KeySize / 8 - offset;
        }

        private static string FormatPublicOrPrivateKey(string privateOrPublicKey)
        {
            return privateOrPublicKey
                .Replace("-----BEGIN PUBLIC KEY-----", "")
                .Replace("-----END PUBLIC KEY-----", "")
                .Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                .Replace("-----END RSA PRIVATE KEY-----", "")
                .Replace("\r", "").Replace("\n", "")
                .Replace("\t", "").Replace(" ", "");
        }

        #region Encrypt by public key
        /// <summary>
        /// 使用公钥 RSA 加密。
        /// </summary>
        /// <param name="plainBytes">明文的字节数组。</param>
        /// <param name="publicKeyBytes">公钥的字节数组。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>密文的字节数组。</returns>
        public static byte[] Encrypt(byte[] plainBytes, byte[] publicKeyBytes, RSAEncryptionPadding encryptionPadding)
        {
            Guard.CheckArgumentNotNull(plainBytes, nameof(plainBytes));
            Guard.CheckArgumentNotNull(publicKeyBytes, nameof(publicKeyBytes));
            Guard.CheckArgumentNotNull(encryptionPadding, nameof(encryptionPadding));

            using (RSA rsa = CreateRsaProviderFromPublicKey(publicKeyBytes))
            {
                int maxBlockSize = GetMaxBlockSize(rsa, encryptionPadding);
                if (plainBytes.Length <= maxBlockSize)
                {
                    return rsa.Encrypt(plainBytes, encryptionPadding);
                }

                using (MemoryStream rStream = new MemoryStream(plainBytes))
                using (MemoryStream wStream = new MemoryStream())
                {
                    byte[] buffer = new byte[maxBlockSize];

                    int blockSize = rStream.Read(buffer, 0, maxBlockSize);

                    while (blockSize > 0)
                    {
                        byte[] blockBytes = new byte[blockSize];
                        Array.Copy(buffer, 0, blockBytes, 0, blockSize);

                        byte[] encryptedBytes = rsa.Encrypt(blockBytes, encryptionPadding);
                        wStream.Write(encryptedBytes, 0, encryptedBytes.Length);

                        blockSize = rStream.Read(buffer, 0, maxBlockSize);
                    }

                    return wStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 使用公钥 RSA 加密。
        /// </summary>
        /// <param name="plainBytes">明文的字节数组。</param>
        /// <param name="publicKeyBytes">公钥的字节数组。</param>
        /// <returns>密文的字节数组。</returns>
        public static byte[] Encrypt(byte[] plainBytes, byte[] publicKeyBytes)
        {
            return Encrypt(plainBytes, publicKeyBytes, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 使用公钥 RSA 加密。
        /// </summary>
        /// <param name="plain">明文。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>密文的 Base64 编码结果。</returns>
        public static string Encrypt(string plain, string publicKey, Encoding encoding, RSAEncryptionPadding encryptionPadding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            publicKey = FormatPublicOrPrivateKey(publicKey);
            byte[] bytes = Encrypt(encoding.GetBytes(plain), Convert.FromBase64String(publicKey), encryptionPadding);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 使用公钥 RSA 加密。
        /// </summary>
        /// <param name="plain">明文。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>密文的 Base64 编码结果。</returns>
        public static string Encrypt(string plain, string publicKey, RSAEncryptionPadding encryptionPadding)
        {
            return Encrypt(plain, publicKey, Encoding.UTF8, encryptionPadding);
        }

        /// <summary>
        /// 使用公钥 RSA 加密。
        /// </summary>
        /// <param name="plain">明文。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <returns>密文的 Base64 编码结果。</returns>
        public static string Encrypt(string plain, string publicKey)
        {
            return Encrypt(plain, publicKey, Encoding.UTF8, RSAEncryptionPadding.Pkcs1);
        }
        #endregion

        #region Decrypt by private key
        /// <summary>
        /// RSA 私钥解密。
        /// </summary>
        /// <param name="cipherBytes">密文的字节数组。</param>
        /// <param name="privateKeyBytes">私钥的字节数组。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>明文的字节数组。</returns>
        public static byte[] Decrypt(byte[] cipherBytes, byte[] privateKeyBytes, RSAEncryptionPadding encryptionPadding)
        {
            Guard.CheckArgumentNotNull(cipherBytes, nameof(cipherBytes));
            Guard.CheckArgumentNotNull(privateKeyBytes, nameof(privateKeyBytes));
            Guard.CheckArgumentNotNull(encryptionPadding, nameof(encryptionPadding));

            using (RSA rsa = CreateRsaProviderFromPrivateKey(privateKeyBytes))
            {
                int maxBlockSize = rsa.KeySize / 8;
                if (cipherBytes.Length <= maxBlockSize)
                {

                    return rsa.Decrypt(cipherBytes, encryptionPadding);
                }

                using (MemoryStream rStream = new MemoryStream(cipherBytes))
                using (MemoryStream wStream = new MemoryStream())
                {
                    byte[] buffer = new byte[maxBlockSize];

                    int blockSize = rStream.Read(buffer, 0, maxBlockSize);

                    while (blockSize > 0)
                    {
                        byte[] blockBytes = new byte[blockSize];
                        Array.Copy(buffer, 0, blockBytes, 0, blockSize);

                        byte[] decryptedBytes = rsa.Decrypt(blockBytes, encryptionPadding);
                        wStream.Write(decryptedBytes, 0, decryptedBytes.Length);

                        blockSize = rStream.Read(buffer, 0, maxBlockSize);
                    }

                    return wStream.ToArray();
                }
            }
        }

        /// <summary>
        /// RSA 私钥解密。
        /// </summary>
        /// <param name="cipherBytes">密文的字节数组。</param>
        /// <param name="privateKeyBytes">私钥的字节数组。</param>
        /// <returns>明文的字节数组。</returns>
        public static byte[] Decrypt(byte[] cipherBytes, byte[] privateKeyBytes)
        {
            return Decrypt(cipherBytes, privateKeyBytes, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// RSA 私钥解密。
        /// </summary>
        /// <param name="cipher">密文的 Base64 编码结果。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>明文。</returns>
        public static string Decrypt(string cipher, string privateKey, Encoding encoding, RSAEncryptionPadding encryptionPadding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            privateKey = FormatPublicOrPrivateKey(privateKey);
            byte[] bytes = Decrypt(Convert.FromBase64String(cipher), Convert.FromBase64String(privateKey), encryptionPadding);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// RSA 私钥解密。
        /// </summary>
        /// <param name="cipher">密文的 Base64 编码结果。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <param name="encryptionPadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>明文。</returns>
        public static string Decrypt(string cipher, string privateKey, RSAEncryptionPadding encryptionPadding)
        {
            return Decrypt(cipher, privateKey, Encoding.UTF8, encryptionPadding);
        }

        /// <summary>
        /// RSA 私钥解密。
        /// </summary>
        /// <param name="cipher">密文的 Base64 编码结果。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <returns>明文。</returns>
        public static string Decrypt(string cipher, string privateKey)
        {
            return Decrypt(cipher, privateKey, Encoding.UTF8, RSAEncryptionPadding.Pkcs1);
        }
        #endregion

        #region Sign by private key
        /// <summary>
        /// 使用私钥 RSA 签名。
        /// </summary>
        /// <param name="bytes">待签名的字节数组。</param>
        /// <param name="privateKeyBytes">私钥的字节数组。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>签名的字节数组。</returns>
        public static byte[] Sign(byte[] bytes, byte[] privateKeyBytes, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            Guard.CheckArgumentNotNull(bytes, nameof(bytes));
            Guard.CheckArgumentNotNull(privateKeyBytes, nameof(privateKeyBytes));
            Guard.CheckArgumentNotNull(signaturePadding, nameof(signaturePadding));

            using (RSA rsa = CreateRsaProviderFromPrivateKey(privateKeyBytes))
            {
                return rsa.SignData(bytes, hashAlgorithm, signaturePadding);
            }
        }

        /// <summary>
        /// 使用私钥 RSA 签名。
        /// </summary>
        /// <param name="bytes">待签名的字节数组。</param>
        /// <param name="privateKeyBytes">私钥的字节数组。</param>
        /// <returns>签名的字节数组。</returns>
        public static byte[] Sign(byte[] bytes, byte[] privateKeyBytes)
        {
            return Sign(bytes, privateKeyBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }

        /// <summary>
        /// 使用私钥 RSA 签名。
        /// </summary>
        /// <param name="data">待签名文本。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>签名的 Base64 编码结果。</returns>
        public static string Sign(string data, string privateKey, Encoding encoding, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            privateKey = FormatPublicOrPrivateKey(privateKey);
            byte[] bytes = Sign(encoding.GetBytes(data), Convert.FromBase64String(privateKey), hashAlgorithm, signaturePadding);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 使用私钥 RSA 签名。
        /// </summary>
        /// <param name="data">待签名文本。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>签名的 Base64 编码结果。</returns>
        public static string Sign(string data, string privateKey, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            return Sign(data, privateKey, Encoding.UTF8, hashAlgorithm, signaturePadding);
        }

        /// <summary>
        /// 使用私钥 RSA 签名。
        /// </summary>
        /// <param name="data">待签名文本。</param>
        /// <param name="privateKey">私钥（PEM 格式）。</param>
        /// <returns>签名的 Base64 编码结果。</returns>
        public static string Sign(string data, string privateKey)
        {
            return Sign(data, privateKey, Encoding.UTF8, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }
        #endregion

        #region Verify by public key
        /// <summary>
        /// 使用公钥 RSA 验证签名。
        /// </summary>
        /// <param name="bytes">待签名的字节数组。</param>
        /// <param name="signBytes">签名的字节数组。</param>
        /// <param name="publicKeyBytes">公钥的字节数组。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>是否合签。</returns>
        public static bool Verify(byte[] bytes, byte[] signBytes, byte[] publicKeyBytes, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            Guard.CheckArgumentNotNull(bytes, nameof(bytes));
            Guard.CheckArgumentNotNull(signBytes, nameof(signBytes));
            Guard.CheckArgumentNotNull(publicKeyBytes, nameof(publicKeyBytes));
            Guard.CheckArgumentNotNull(signaturePadding, nameof(signaturePadding));

            using (RSA rsa = CreateRsaProviderFromPublicKey(publicKeyBytes))
            {
                return rsa.VerifyData(bytes, signBytes, hashAlgorithm, signaturePadding);
            }
        }

        /// <summary>
        /// 使用公钥 RSA 验证签名。
        /// </summary>
        /// <param name="bytes">待签名的字节数组。</param>
        /// <param name="signBytes">签名的字节数组。</param>
        /// <param name="publicKeyBytes">公钥的字节数组。</param>
        /// <returns>是否合签。</returns>
        public static bool Verify(byte[] bytes, byte[] signBytes, byte[] publicKeyBytes)
        {
            return Verify(bytes, signBytes, publicKeyBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }

        /// <summary>
        /// 使用公钥 RSA 验证签名。
        /// </summary>
        /// <param name="data">密文的 Base64 编码结果。</param>
        /// <param name="sign">签名的 Base64 编码结果。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <param name="encoding">字符集（默认 <see cref="Encoding.UTF8" />）。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>是否合签。</returns>
        public static bool Verify(string data, string sign, string publicKey, Encoding encoding, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            publicKey = FormatPublicOrPrivateKey(publicKey);
            return Verify(encoding.GetBytes(data), Convert.FromBase64String(sign), Convert.FromBase64String(publicKey), hashAlgorithm, signaturePadding);
        }

        /// <summary>
        /// 使用公钥 RSA 验证签名。
        /// </summary>
        /// <param name="data">密文的 Base64 编码结果。</param>
        /// <param name="sign">签名的 Base64 编码结果。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <param name="hashAlgorithm">哈希算法（默认 <see cref="HashAlgorithmName.SHA1" />）</param>
        /// <param name="signaturePadding">对齐方式（默认 <see cref="RSAEncryptionPadding.Pkcs1" />）。</param>
        /// <returns>是否合签。</returns>
        public static bool Verify(string data, string sign, string publicKey, HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            return Verify(data, sign, publicKey, Encoding.UTF8, hashAlgorithm, signaturePadding);
        }

        /// <summary>
        /// 使用公钥 RSA 验证签名。
        /// </summary>
        /// <param name="data">密文的 Base64 编码结果。</param>
        /// <param name="sign">签名的 Base64 编码结果。</param>
        /// <param name="publicKey">公钥（PEM 格式）。</param>
        /// <returns>是否合签。</returns>
        public static bool Verify(string data, string sign, string publicKey)
        {
            return Verify(data, sign, publicKey, Encoding.UTF8, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }
        #endregion
    }
}
#endif
