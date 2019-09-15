using Xunit;

namespace STEP.Utility.UnitTests
{
    public class RegexUtilUnitTests
    {
        [Fact]
        public void IsNumericTest()
        {
            Assert.True(RegexUtil.IsNumeric("1"));
            Assert.True(RegexUtil.IsNumeric("0.1"));
            Assert.True(RegexUtil.IsNumeric("-1"));
            Assert.True(RegexUtil.IsNumeric("-0.1"));
            Assert.True(RegexUtil.IsNumeric("+1"));
            Assert.True(RegexUtil.IsNumeric("+0.1"));

            Assert.False(RegexUtil.IsNumeric(" "));
            Assert.False(RegexUtil.IsNumeric(" 1"));
            Assert.False(RegexUtil.IsNumeric("1 "));
            Assert.False(RegexUtil.IsNumeric("a"));
            Assert.False(RegexUtil.IsNumeric("0.a"));
            Assert.False(RegexUtil.IsNumeric("-a"));
            Assert.False(RegexUtil.IsNumeric("0.1."));
            Assert.False(RegexUtil.IsNumeric("0.1.2"));
            Assert.False(RegexUtil.IsNumeric("--1"));
            Assert.False(RegexUtil.IsNumeric("++1"));
        }

        [Fact]
        public void IsIntegerTest()
        {
            Assert.True(RegexUtil.IsInteger("1"));
            Assert.True(RegexUtil.IsInteger("-1"));
            Assert.True(RegexUtil.IsInteger("+1"));

            Assert.False(RegexUtil.IsInteger("0.1"));
            Assert.False(RegexUtil.IsInteger("-0.1"));
            Assert.False(RegexUtil.IsInteger("+0.1"));
        }

        [Fact]
        public void IsUnsignNumericTest()
        {
            Assert.True(RegexUtil.IsUnsignNumeric("1"));
            Assert.True(RegexUtil.IsUnsignNumeric("0.1"));
            Assert.True(RegexUtil.IsUnsignNumeric("+1"));
            Assert.True(RegexUtil.IsUnsignNumeric("+0.1"));

            Assert.False(RegexUtil.IsUnsignNumeric("-1"));
            Assert.False(RegexUtil.IsUnsignNumeric("-0.1"));
        }

        [Fact]
        public void IsUnsignIntegerTest()
        {
            Assert.True(RegexUtil.IsUnsignInteger("1"));
            Assert.True(RegexUtil.IsUnsignInteger("+1"));

            Assert.False(RegexUtil.IsUnsignInteger("-1"));
            Assert.False(RegexUtil.IsUnsignInteger("0.1"));
            Assert.False(RegexUtil.IsUnsignInteger("-0.1"));
            Assert.False(RegexUtil.IsUnsignInteger("+0.1"));
        }

        [Fact]
        public void IsQQTest()
        {
            Assert.True(RegexUtil.IsQQ("12345"));
            Assert.True(RegexUtil.IsQQ("601181332"));

            Assert.False(RegexUtil.IsQQ("1234"));
            Assert.False(RegexUtil.IsQQ("12345678901"));
            Assert.False(RegexUtil.IsQQ("abcde"));
            Assert.False(RegexUtil.IsQQ("12345abcde"));
        }

        [Fact]
        public void IsPRCTelephoneTest()
        {
            Assert.True(RegexUtil.IsPRCTelephone("12345678"));
            Assert.True(RegexUtil.IsPRCTelephone("010-12345678"));
            Assert.True(RegexUtil.IsPRCTelephone("0411-12345678"));

            Assert.False(RegexUtil.IsPRCTelephone("12345"));
            Assert.False(RegexUtil.IsPRCTelephone("010-12345"));
            Assert.False(RegexUtil.IsPRCTelephone("0411-12345"));
            Assert.False(RegexUtil.IsPRCTelephone("12345abcde"));
            Assert.False(RegexUtil.IsPRCTelephone("1234567890123456789"));
        }

        [Fact]
        public void IsPRCMobilephoneTest()
        {
            Assert.True(RegexUtil.IsPRCMobilephone("18012345678"));

            Assert.False(RegexUtil.IsPRCMobilephone("1801234567"));
            Assert.False(RegexUtil.IsPRCMobilephone("180123456789"));
            Assert.False(RegexUtil.IsPRCMobilephone("1801234567a"));
            Assert.False(RegexUtil.IsPRCMobilephone("28012345678"));
        }

        [Fact]
        public void IsEmailTest()
        {
            Assert.True(RegexUtil.IsEmail("test@abc.org"));
            Assert.True(RegexUtil.IsEmail("test@abc.com.cn"));

            Assert.False(RegexUtil.IsEmail("test"));
            Assert.False(RegexUtil.IsEmail("test@"));
            Assert.False(RegexUtil.IsEmail("test@abc"));
            Assert.False(RegexUtil.IsEmail("test@abc.org&12"));
        }

        [Fact]
        public void IsIPv4Test()
        {
            Assert.True(RegexUtil.IsIPv4("172.16.254.1"));

            Assert.False(RegexUtil.IsIPv4("172.316.254.1"));
            Assert.False(RegexUtil.IsIPv4(".254.255.0"));
            Assert.False(RegexUtil.IsIPv4("1.1.1.1a"));
        }

        [Fact]
        public void IsIPv6Test()
        {
            Assert.True(RegexUtil.IsIPv6("::1"));
            Assert.True(RegexUtil.IsIPv6("2a02:2770::21a:4aff:feb3:2ee"));

            Assert.False(RegexUtil.IsIPv6("172.316.254.1"));
            Assert.False(RegexUtil.IsIPv6("2a02:2770"));
        }

        [Fact]
        public void FilterHtmlTagsTest()
        {
            Assert.Equal("helloworld", RegexUtil.FilterHtmlTags("<div><div class=\"view\">hello</div><div>world</div></div>"));
        }

        [Fact]
        public void FilterWhiteSpacesTest()
        {
            Assert.Equal("helloworld", RegexUtil.FilterWhiteSpaces("hello \r\nworld"));
        }

        [Fact]
        public void FilterLineBreaksTest()
        {
            Assert.Equal("hello world", RegexUtil.FilterLineBreaks("hello \r\nworld"));
        }

        [Fact]
        public void MatchTest()
        {
            Assert.Equal("image/ad1.gif", RegexUtil.Match(
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", 
                "<img src=image/ad1.gif width=\"128\" height=\"36\"/><img src='image/ad2.gif' />"
            ));
        }
    }
}
