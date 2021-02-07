using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class CaptchaUtilUnitTests
    {
        [Fact]
        public void GenerateTest()
        {
            const int LEN = 10;
            Assert.Equal(LEN, CaptchaUtil.GetCaptcha(LEN).Length);
        }
    }
}
