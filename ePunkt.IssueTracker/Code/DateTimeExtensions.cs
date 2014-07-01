using System;

namespace ePunkt.IssueTracker.Code
{
    public static class DateTimeExtensions
    {
        public static DateTime ToCentralEuropeanTime(this DateTime date)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(date, timeZone);
        }
    }
}