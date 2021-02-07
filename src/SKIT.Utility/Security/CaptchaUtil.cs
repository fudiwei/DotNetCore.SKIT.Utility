using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// 验证码工具类。
    /// </summary>
    public static class CaptchaUtil
    {
        static readonly string CAPTCHA_SEED = "0123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
        static readonly int CAPTCHA_SEED_LENGTH = CAPTCHA_SEED.Length;

        /// <summary>
        /// 生成指定长度的随机字符串。
        /// </summary>
        /// <param name="len">指定长度。</param>
        /// <returns>随机字符串。</returns>
        public static string GetCaptcha(int len)
        {
            Guard.CheckArgumentInRange(len, nameof(len), (i) => i >= 0);

            StringBuilder result = new StringBuilder();
            while (result.Length < len)
            {
                result.Append(CAPTCHA_SEED[(int)RngUtil.GetRandom(CAPTCHA_SEED_LENGTH - 1)]);
            }
            return result.ToString();
        }
    }
}
