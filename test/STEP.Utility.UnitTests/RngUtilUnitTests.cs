using Xunit;

namespace STEP.Utility.UnitTests
{
    public class RngUtilUnitTests
    {
        [Fact]
        public void GenerateTest()
        {
            const int MIN = 100;
            const int MAX = 999;

            for (int i = 0; i < 1000; i++)
            {
                Assert.InRange(RngUtil.GetRandom(MIN, MAX), MIN, MAX);
            }
        }
    }
}
