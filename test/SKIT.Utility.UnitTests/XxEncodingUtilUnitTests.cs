using System.Linq;
using System.IO;
using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class XxEncodingUtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED = "hello world";
            const string STR_ENCODED = "9O4JgP4wURqxmP4E+";

            Assert.Equal(STR_ENCODED, XxEncodingUtil.Encode(STR_DECODED));
            Assert.Equal(STR_DECODED, XxEncodingUtil.Decode(STR_ENCODED));
        }
    }
}
