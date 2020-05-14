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

        [Fact]
        public void ValidateTest()
        {
            const string STR_1 = "aGVsbG8gd29ybGQ=";
            const string STR_2 = "aGVsbG8gd29ybGQ";
            const string STR_3 = "aGVsbG8gd29ybGQ.";
            const string STR_4 = "aGVsbG8gd29ybG=Q";

            Assert.True(Base64Util.TryDecode(STR_1, out _));
            Assert.False(Base64Util.TryDecode(STR_2, out _));
            Assert.False(Base64Util.TryDecode(STR_3, out _));
            Assert.False(Base64Util.TryDecode(STR_4, out _));
        }
    }
}
