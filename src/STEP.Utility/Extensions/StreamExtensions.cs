using STEP.Utility;

namespace System.IO
{
    /// <summary>
    /// 
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 将若干个字节写入流内的当前位置，并将流内的位置向前提升若干个字节。
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="bytes">要写入流中的字节数组。</param>
        public static void WriteBytes(this Stream stream, byte[] bytes)
        {
            Guard.CheckArgumentNotNull(bytes, nameof(bytes));

            for (int i = 0, len = bytes.Length; i < len; i++)
            {
                stream.WriteByte(bytes[i]);
            }
        }

        /// <summary>
        /// 将流内容写入字节数组，忽略 System.IO.Stream.Position 的设置。
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            long offset = stream.Position;

            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            if (stream.CanSeek)
                stream.Seek(offset, SeekOrigin.Current);

            return bytes;
        }
    }
}
