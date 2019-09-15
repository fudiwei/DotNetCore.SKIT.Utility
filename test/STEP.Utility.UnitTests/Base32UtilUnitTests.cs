using Xunit;

namespace STEP.Utility.UnitTests
{
    public class Base32UtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED = "hello world";
            const string STR_ENCODED = "NBSWY3DPEB3W64TMMQ======";

            Assert.Equal(STR_ENCODED, Base32Util.Encode(STR_DECODED));
            Assert.Equal(STR_DECODED, Base32Util.Decode(STR_ENCODED));
        }
    }
}
