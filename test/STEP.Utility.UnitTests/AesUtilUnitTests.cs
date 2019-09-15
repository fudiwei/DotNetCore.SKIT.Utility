using Xunit;

namespace STEP.Utility.UnitTests
{
    public class CaesarCipherUtilUnitTests
    {
        [Fact]
        public void EncryptTest()
        {
            Assert.Equal("khoor, zruog.", CaesarCipherUtil.Encrypt("hello, world.", 3));
        }
    }
}
