using System.Security.Cryptography;
using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class AesUtilUnitTests
    {
        const string STR_EN = "hello, world.";
        const string STR_CH = "��ã����硣";
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
             * AES �� 16 �ֽ�Ϊһ�飬�����һ�鲻�� 16 �ֽڻ���ݶ��뷽ʽ���룬
             * ���½��ܺ���ܺ�ԭ����Ƚ�β������ɸ��հ��ַ���
             */

            Assert.Equal(STR_EN, AesUtil.Decrypt("OiGFDP47HdtJxgDbs/KvUQ==", SECRET).TrimEnd('\0'));
            Assert.Equal(STR_CH, AesUtil.Decrypt("eyrfpEEEqwdXvtHoRUF7XgeN49Ug+v7LxMDf/4An0bU=", SECRET).TrimEnd('\0'));
        }
    }
}
