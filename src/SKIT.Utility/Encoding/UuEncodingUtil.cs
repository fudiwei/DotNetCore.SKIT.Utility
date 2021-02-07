using System;
using System.IO;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// UU 编码（Unix-to-Unix Encoding）工具类。
    /// </summary>
    public static class UuEncodingUtil
    {
        private static readonly byte[] UUEncMap = new byte[]
        {
            0x60, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
            0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
            0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47,
            0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57,
            0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F
        };

        private static readonly byte[] UUDecMap = new byte[]
        {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
            0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
            0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
            0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        /// <summary>
        /// 将数据进行 UU 编码。
        /// </summary>
        /// <param name="bytes">待编码数据的字节数组。</param>
        /// <returns></returns>
        public static byte[] Encode(byte[] bytes)
        {
            Guard.CheckArgumentNotNull(bytes, nameof(bytes));

            int sidx = 0;
            int lineLen = 45;
            byte[] nl = Encoding.ASCII.GetBytes(Environment.NewLine);

            byte A, B, C;

            using (MemoryStream input = new MemoryStream(bytes))
            using (MemoryStream output = new MemoryStream())
            {
                long len = input.Length;
                if (len == 0)
                    return new byte[0];

                while (sidx + lineLen < len)
                {
                    output.WriteByte(UUEncMap[lineLen]);

                    for (int end = sidx + lineLen; sidx < end; sidx += 3)
                    {
                        A = (byte)input.ReadByte();
                        B = (byte)input.ReadByte();
                        C = (byte)input.ReadByte();

                        output.WriteByte(UUEncMap[(A >> 2) & 63]);
                        output.WriteByte(UUEncMap[(B >> 4) & 15 | (A << 4) & 63]);
                        output.WriteByte(UUEncMap[(C >> 6) & 3 | (B << 2) & 63]);
                        output.WriteByte(UUEncMap[C & 63]);
                    }

                    output.WriteBytes(nl);
                }

                output.WriteByte(UUEncMap[len - sidx]);

                while (sidx + 2 < len)
                {
                    A = (byte)input.ReadByte();
                    B = (byte)input.ReadByte();
                    C = (byte)input.ReadByte();

                    output.WriteByte(UUEncMap[(A >> 2) & 63]);
                    output.WriteByte(UUEncMap[(B >> 4) & 15 | (A << 4) & 63]);
                    output.WriteByte(UUEncMap[(C >> 6) & 3 | (B << 2) & 63]);
                    output.WriteByte(UUEncMap[C & 63]);
                    sidx += 3;
                }

                if (sidx < len - 1)
                {
                    A = (byte)input.ReadByte();
                    B = (byte)input.ReadByte();

                    output.WriteByte(UUEncMap[(A >> 2) & 63]);
                    output.WriteByte(UUEncMap[(B >> 4) & 15 | (A << 4) & 63]);
                    output.WriteByte(UUEncMap[(B << 2) & 63]);
                    output.WriteByte(UUEncMap[0]);
                }
                else if (sidx < len)
                {
                    A = (byte)input.ReadByte();

                    output.WriteByte(UUEncMap[(A >> 2) & 63]);
                    output.WriteByte(UUEncMap[(A << 4) & 63]);
                    output.WriteByte(UUEncMap[0]);
                    output.WriteByte(UUEncMap[0]);
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// 将字符串进行基于指定字符集的 UU 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Encode(string text, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] bytes = Encode(encoding.GetBytes(text));
            return string.IsNullOrEmpty(text) ? string.Empty : encoding.GetString(bytes);
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 UU 编码。
        /// </summary>
        /// <param name="text">待编码的字符串。</param>
        /// <returns></returns>
        public static string Encode(string text)
        {
            return Encode(text, Encoding.UTF8);
        }

        /// <summary>
        /// 将数据进行 UU 解码。
        /// </summary>
        /// <param name="encodedBytes">待解码数据的字节数组。</param>
        /// <returns></returns>
        public static byte[] Decode(byte[] encodedBytes)
        {
            Guard.CheckArgumentNotNull(encodedBytes, nameof(encodedBytes));

            using (MemoryStream input = new MemoryStream(encodedBytes))
            using (MemoryStream output = new MemoryStream())
            {
                long len = input.Length;
                if (len == 0)
                    return new byte[0];

                long didx = 0;
                int nextByte = input.ReadByte();
                while (nextByte >= 0)
                {
                    int lineLen = UUDecMap[nextByte];

                    long end = didx + lineLen;
                    byte A, B, C, D;
                    if (end > 2)
                    {
                        while (didx < end - 2)
                        {
                            A = UUDecMap[input.ReadByte()];
                            B = UUDecMap[input.ReadByte()];
                            C = UUDecMap[input.ReadByte()];
                            D = UUDecMap[input.ReadByte()];

                            output.WriteByte((byte)(((A << 2) & 255) | ((B >> 4) & 3)));
                            output.WriteByte((byte)(((B << 4) & 255) | ((C >> 2) & 15)));
                            output.WriteByte((byte)(((C << 6) & 255) | (D & 63)));
                            didx += 3;
                        }
                    }

                    if (didx < end)
                    {
                        A = UUDecMap[input.ReadByte()];
                        B = UUDecMap[input.ReadByte()];
                        output.WriteByte((byte)(((A << 2) & 255) | ((B >> 4) & 3)));
                        didx++;

                        if (didx < end)
                        {
                            C = UUDecMap[input.ReadByte()];
                            output.WriteByte((byte)(((B << 4) & 255) | ((C >> 2) & 15)));
                            didx++;
                        }
                    }

                    do
                    {
                        nextByte = input.ReadByte();
                    }
                    while (nextByte >= 0 && nextByte != '\n' && nextByte != '\r');

                    do
                    {
                        nextByte = input.ReadByte();
                    }
                    while (nextByte >= 0 && (nextByte == '\n' || nextByte == '\r'));
                }

                return output.ToArray();
            }
        }

        /// <summary>
        /// 将字符串进行基于指定字符集的 UU 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <param name="encoding">字符集。</param>
        /// <returns></returns>
        public static string Decode(string encodedText, Encoding encoding)
        {
            Guard.CheckArgumentNotNull(encoding, nameof(encoding));

            byte[] bytes = Decode(encoding.GetBytes(encodedText));
            return string.IsNullOrEmpty(encodedText) ? string.Empty : encoding.GetString(bytes);
        }

        /// <summary>
        /// 将字符串进行基于 UTF-8 的 UU 解码。
        /// </summary>
        /// <param name="encodedText">待解码的字符串。</param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
            return Decode(encodedText, Encoding.UTF8);
        }
    }
}
