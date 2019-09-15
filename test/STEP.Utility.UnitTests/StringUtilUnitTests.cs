using Xunit;

namespace STEP.Utility.UnitTests
{
    public class StringUtilUnitTests
    {
        [Fact]
        public void StringByteTest()
        {
            const char CHR_SINGLE = 'a';
            const char CHR_DOUBLE = '£á';
            const string STR_SINGLE = "hello";
            const string STR_DOUBLE = "£è£å£ì£ì£ï";

            Assert.True(StringUtil.IsDoubleByte(CHR_DOUBLE));
            Assert.True(StringUtil.IsDoubleByte(STR_DOUBLE));
            Assert.False(StringUtil.IsDoubleByte(CHR_SINGLE));
            Assert.False(StringUtil.IsDoubleByte(STR_SINGLE));
            Assert.False(StringUtil.IsDoubleByte(STR_SINGLE + CHR_DOUBLE));

            Assert.Equal(CHR_SINGLE, StringUtil.ToSingleByte(CHR_DOUBLE));
            Assert.Equal(CHR_DOUBLE, StringUtil.ToDoubleByte(CHR_SINGLE));
            Assert.Equal(STR_SINGLE, StringUtil.ToSingleByte(STR_DOUBLE));
            Assert.Equal(STR_DOUBLE, StringUtil.ToDoubleByte(STR_SINGLE));

            Assert.Equal(5, StringUtil.GetByteLength(STR_SINGLE));
            Assert.Equal(10, StringUtil.GetByteLength(STR_DOUBLE));
        }

        [Fact]
        public void TruncateTest()
        {
            const string STR = "hello world";

            Assert.Equal("world", StringUtil.Substring(STR, 6, 5));
            Assert.Equal("world", StringUtil.Substring(STR, 6, 6));
            Assert.Equal("", StringUtil.Substring(STR, 11, 5));

            Assert.Equal("hello", StringUtil.Left(STR, 5));
            Assert.Equal("hello world", StringUtil.Left(STR, 12));

            Assert.Equal("world", StringUtil.Right(STR, 5));
            Assert.Equal("hello world", StringUtil.Right(STR, 12));
        }

        [Fact]
        public void ReverseTest()
        {
            Assert.Equal("321", StringUtil.Reverse("123"));
        }
    }
}
