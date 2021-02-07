using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class Base16UtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED = "hello world";
            const string STR_ENCODED = "68656C6C6F20776F726C64";

            Assert.Equal(STR_ENCODED, Base16Util.Encode(STR_DECODED));
            Assert.Equal(STR_DECODED, Base16Util.Decode(STR_ENCODED));
        }
    }
}
