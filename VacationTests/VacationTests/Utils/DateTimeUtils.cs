using System;

namespace VacationTests.Utils
{
    public static class DateTimeUtils
    {
        private const string DateFormat = "dd.MM.yyyy";
        public static DateTime GetStartDateFromNow(int daysShift = 5)
        {
            return DateTime.Now.AddDays(daysShift);
        }

        public static DateTime GetEndDateFromNow(int daysShift = 12)
        {
            return DateTime.Now.AddDays(daysShift);
        }
        
        public static string GetPeriod(DateTime startDate, DateTime endDate)
        {
            return $@"{startDate.ToString(DateFormat)} - {endDate.ToString(DateFormat)}";
        }
        
        public static string GetPeriod(DateTime startDate, DateTime endDate, string separator = "-")
        {
            return $@"{startDate.ToString(DateFormat)} {separator} {endDate.ToString(DateFormat)}";
        }
    }
}