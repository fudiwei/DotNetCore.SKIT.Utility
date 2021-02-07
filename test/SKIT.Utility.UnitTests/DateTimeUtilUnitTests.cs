using System;
using Xunit;

namespace SKIT.Utility.UnitTests
{
    public class DateTimeUtilUnitTests
    {
        [Fact()]
        public void TimestampTest()
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            Assert.Equal(0, DateTimeUtil.ToUnixTimeSeconds(start));
            Assert.Equal(start, DateTimeUtil.FromUnixTimeSeconds(0).ToUniversalTime());
        }

        [Fact()]
        public void DayOfWeekTest()
        {
            Assert.Equal(new DateTime(2019, 9, 8), DateTimeUtil.GetFirstDayOfWeek(new DateTime(2019, 9, 10)));
            Assert.Equal(new DateTime(2019, 9, 14), DateTimeUtil.GetLastDayOfWeek(new DateTime(2019, 9, 10)));
        }

        [Fact()]
        public void DiffTextTest()
        {
            DateTime now = DateTime.Now;

            Assert.Equal("�ո�", DateTimeUtil.GetDiffText(now.AddSeconds(-1)));
            Assert.Equal("2����ǰ", DateTimeUtil.GetDiffText(now.AddSeconds(-1).AddMinutes(-2)));
            Assert.Equal("3Сʱǰ", DateTimeUtil.GetDiffText(now.AddSeconds(-1).AddMinutes(-2).AddHours(-3)));
            Assert.Equal("4��ǰ", DateTimeUtil.GetDiffText(now.AddSeconds(-1).AddMinutes(-2).AddHours(-3).AddDays(-4)));
            Assert.Equal("5����ǰ", DateTimeUtil.GetDiffText(now.AddSeconds(-1).AddMinutes(-2).AddHours(3).AddDays(-4).AddMonths(-5)));
            Assert.Equal("6��ǰ", DateTimeUtil.GetDiffText(now.AddSeconds(-1).AddMinutes(-2).AddHours(-3).AddDays(-4).AddMonths(-5).AddYears(-6)));
        }
    }
}
