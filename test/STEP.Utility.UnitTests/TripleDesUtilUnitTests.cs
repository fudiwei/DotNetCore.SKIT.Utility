using System.Security.Cryptography;
using Xunit;

namespace STEP.Utility.UnitTests
{
    public class TripleDesUtilUnitTests
    {
        const string STR_EN = "hello, world.";
        const string STR_CH = "ÄãºÃ£¬ÊÀ½ç¡£";
        const string SECRET = "00000000mysecret00000000";

        [Fact]
        public void EncryptTest()
        {
            Assert.Equal("M2if+2s/J+86CzAOwGHtWA==", TripleDesUtil.Encrypt(STR_EN, SECRET));
            Assert.Equal("ef748iOSM8I22ItZEFQMUSYSg7wUsMMr", TripleDesUtil.Encrypt(STR_CH, SECRET));
        }

        [Fact]
        public void DecryptTest()
        {
            Assert.Equal(STR_EN, TripleDesUtil.Decrypt("M2if+2s/J+86CzAOwGHtWA==", SECRET));
            Assert.Equal(STR_CH, TripleDesUtil.Decrypt("ef748iOSM8I22ItZEFQMUSYSg7wUsMMr", SECRET));
        }
    }
}
