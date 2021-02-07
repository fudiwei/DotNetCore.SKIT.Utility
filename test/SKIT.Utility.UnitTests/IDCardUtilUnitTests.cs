using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class IDCardUtilUnitTests
    {
        [Fact]
        public void IDNumber15Test()
        {
            Assert.True(IDCardUtil.IsIDNumber15("110101000101100"));

            Assert.False(IDCardUtil.IsIDNumber15("990101000101100"));
            Assert.False(IDCardUtil.IsIDNumber15("119901001301100"));
            Assert.False(IDCardUtil.IsIDNumber15("11990100130110A"));
            Assert.False(IDCardUtil.IsIDNumber15("11010100010110"));
            Assert.False(IDCardUtil.IsIDNumber15("1101010001011000"));
        }

        [Fact]
        public void IDNumber18Test()
        {
            Assert.True(IDCardUtil.IsIDNumber15("110101000101100"));

            Assert.False(IDCardUtil.IsIDNumber18("990101190001011009"));
            Assert.False(IDCardUtil.IsIDNumber18("110101190013011009"));
            Assert.False(IDCardUtil.IsIDNumber18("110101190001011000"));
            Assert.False(IDCardUtil.IsIDNumber18("11010119000101100A"));
            Assert.False(IDCardUtil.IsIDNumber18("11010119000101100"));
            Assert.False(IDCardUtil.IsIDNumber18("1101011900010110090"));
        }
    }
}
