namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// 判断当前 <see cref="System.Uri"/> 对象的方案名称是否是 HTTP / HTTPS。
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsHttpOrHttps(this Uri uri)
        {
            return "http".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase) ||
                "https".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 判断当前 <see cref="System.Uri"/> 对象的方案名称是否是 FTP。
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static bool IsFtp(this Uri uri)
        {
            return "ftp".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase);
        }
    }
}
