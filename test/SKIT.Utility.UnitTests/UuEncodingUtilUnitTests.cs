using System.Linq;
using System.IO;
using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class UuEncodingUtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED = "hello world";
            const string STR_ENCODED = "+:&5L;&\\@=V]R;&0`";

            Assert.Equal(STR_ENCODED, UuEncodingUtil.Encode(STR_DECODED));
            Assert.Equal(STR_DECODED, UuEncodingUtil.Decode(STR_ENCODED));
        }
    }
}
