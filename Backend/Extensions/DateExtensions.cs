using System;

namespace EfficiencyApp.Extensions
{
    public static class DateExtensions
    {

        public static DateTime LocalFromUtc(this DateTime date)
        {
            var localZone = TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTimeFromUtc(date, localZone);
        }

    }
}
