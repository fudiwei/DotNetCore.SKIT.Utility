using Xunit;

namespace STEP.Utility.UnitTests
{
    public class Base64UtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED = "hello world";
            const string STR_ENCODED = "aGVsbG8gd29ybGQ=";

            Assert.Equal(STR_ENCODED, Base64Util.Encode(STR_DECODED));
            Assert.Equal(STR_DECODED, Base64Util.Decode(STR_ENCODED));
        }
    }
}
