using System.Security.Cryptography;
using Xunit;

namespace STEP.Utility.UnitTests
{
    public class AesUtilUnitTests
    {
        const string STR_EN = "hello, world.";
        const string STR_CH = "你好，世界。";
        const string SECRET = "mysecret00000000";

        [Fact]
        public void EncryptTest()
        {
            Assert.Equal("OiGFDP47HdtJxgDbs/KvUQ==", AesUtil.Encrypt(STR_EN, SECRET));
            Assert.Equal("eyrfpEEEqwdXvtHoRUF7XgeN49Ug+v7LxMDf/4An0bU=", AesUtil.Encrypt(STR_CH, SECRET));
        }

        [Fact]
        public void DecryptTest()
        {
            /*
             * AES 以 16 字节为一块，当最后一块不足 16 字节会根据对齐方式补齐，
             * 导致解密后可能和原文相比结尾多出若干个空白字符。
             */

            Assert.Equal(STR_EN, AesUtil.Decrypt("OiGFDP47HdtJxgDbs/KvUQ==", SECRET).TrimEnd('\0'));
            Assert.Equal(STR_CH, AesUtil.Decrypt("eyrfpEEEqwdXvtHoRUF7XgeN49Ug+v7LxMDf/4An0bU=", SECRET).TrimEnd('\0'));
        }
    }
}
