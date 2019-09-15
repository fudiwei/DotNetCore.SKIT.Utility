using System;
using System.Text;

namespace STEP.Utility
{
    /// <summary>
    /// 凯撒密码（Caesar Cipher）工具类。
    /// </summary>
    public static class CaesarCipherUtil
    {
        /// <summary>
        /// 凯撒加密。
        /// </summary>
        /// <param name="text">明文。</param>
        /// <param name="offset">偏移量。</param>
        /// <returns>密文。</returns>
        public static string Encrypt(string text, int offset)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder builder = new StringBuilder();
            foreach (char chr in text)
            {
                if (char.IsLetter(chr))
                {
                    char d = char.IsUpper(chr) ? 'A' : 'a';
                    d = (char)((((chr + offset) - d) % 26) + d);
                    builder.Append(d);
                }
                else
                {
                    builder.Append(chr);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 凯撒解密。
        /// </summary>
        /// <param name="cipher">密文。</param>
        /// <param name="offset">偏移量。</param>
        /// <returns>密文。</returns>
        public static string Decrypt(string cipher, int offset)
        {
            return Encrypt(cipher, 26 - offset);
        }
    }
}