using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class PercentEncodingUtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            const string STR_DECODED_EN = "C# is the best!";
            const string STR_ENCODED_EN = "C%23%20is%20the%20best%21";
            const string STR_DECODED_CH = "C# ����õ����ԣ�";
            const string STR_ENCODED_CH = "C%23%20%E6%98%AF%E6%9C%80%E5%A5%BD%E7%9A%84%E8%AF%AD%E8%A8%80%EF%BC%81";

            Assert.Equal(STR_ENCODED_EN, PercentEncodingUtil.Encode(STR_DECODED_EN));
            Assert.Equal(STR_DECODED_EN, PercentEncodingUtil.Decode(STR_ENCODED_EN));
            Assert.Equal(STR_ENCODED_CH, PercentEncodingUtil.Encode(STR_DECODED_CH));
            Assert.Equal(STR_DECODED_CH, PercentEncodingUtil.Decode(STR_ENCODED_CH));
        }
    }
}
