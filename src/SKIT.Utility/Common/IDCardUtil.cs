using System;
using System.Collections.Generic;
using System.Text;

namespace SKIT.Utility
{
    /// <summary>
    /// 身份证号工具类。
    /// </summary>
    public static class IDCardUtil
    {
        private const string ADDRESS = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

        /// <summary>
        /// 判断字符是否为 15 位身份证号。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIDNumber15(string value)
        {
            if (value == null) return false;
            if (value.Length != 15) return false;
            
            if (long.TryParse(value, out long n) == false || n < Math.Pow(10, 14))
            {
                return false; // 数字验证  
            }
            
            if (ADDRESS.IndexOf(value.Remove(2)) == -1)
            {
                return false; // 省份验证  
            }

            string birth = value.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            if (!DateTime.TryParse(birth, out _))
            {
                return false; // 生日验证  
            }

            return true;
        }

        /// <summary>
        /// 判断字符是否为 15/18 位身份证号。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsIDNumber18(string value)
        {
            if (value == null) return false;
            if (value.Length != 18) return false;
            
            if (long.TryParse(value.Remove(17), out long n) == false
                || n < Math.Pow(10, 16) || long.TryParse(value.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false; // 数字验证  
            }

            if (ADDRESS.IndexOf(value.Remove(2)) == -1)
            {
                return false; // 省份验证  
            }

            string birth = value.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            if (!DateTime.TryParse(birth, out _))
            {
                return false; // 生日验证  
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = value.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
#if NETSTANDARD1_6
            y = sum % 11;
#else
            Math.DivRem(sum, 11, out y);
#endif
            if (arrVarifyCode[y] != value.Substring(17, 1).ToLower())
            {
                return false; // 校验码验证  
            }

            return true;
        }
    }
}
