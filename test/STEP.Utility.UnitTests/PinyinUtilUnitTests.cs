using Xunit;

namespace STEP.Utility.UnitTests
{
    public class PinyinUtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            Assert.Equal("TianDiXuanHuang YuZhouHongHuang", PinyinUtil.GetFullChars("天地玄黄 宇宙洪荒"));
            Assert.Equal("RZC XBS XXJ XXY", PinyinUtil.GetFirstChars("人之初 性本善 性相近 习相远"));
        }
    }
}
