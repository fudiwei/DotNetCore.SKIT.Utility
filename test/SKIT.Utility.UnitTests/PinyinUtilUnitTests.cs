using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class PinyinUtilUnitTests
    {
        [Fact]
        public void ConvertTest()
        {
            Assert.Equal("TianDiXuanHuang YuZhouHongHuang", PinyinUtil.GetFullChars("������� ������"));
            Assert.Equal("RZC XBS XXJ XXY", PinyinUtil.GetFirstChars("��֮�� �Ա��� ����� ϰ��Զ"));
        }
    }
}
