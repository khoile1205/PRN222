using System.Globalization;

namespace BussinessLayer.Helper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets the start of the week containing the specified date
        /// </summary>
        public static DateTime GetStartOfWeek(DateTime date)
        {
            // Adjust to the start of the week (Monday as first day)
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Gets the start of the month containing the specified date
        /// </summary>
        public static DateTime GetStartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Gets the start of the month containing month and year
        /// </summary>
        public static DateTime GetStartOfMonth(int month, int year)
        {
            return new DateTime(year, month, 1);
        }

        /// <summary>
        /// Returns the last moment of the last day of the month for the specified date.
        /// The returned DateTime is set to 23:59:59 (11:59:59 PM) of the final day of the month.
        /// </summary>
        /// <param name="date">The date used to determine the year and month. The day component is ignored.</param>
        /// <returns>A DateTime representing the end of the specified month (e.g., 2025-03-31 23:59:59).</returns>
        public static DateTime GetEndOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month))
                .AddHours(23)
                .AddMinutes(59)
                .AddSeconds(59);
        }

        public static DateTime GetEndOfMonth(int month, int year)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month))
                .AddHours(23)
                .AddMinutes(59)
                .AddSeconds(59);
        }

        /// <summary>
        /// Gets the quarter (1-4) for the specified date
        /// </summary>
        public static int GetQuarter(DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        /// <summary>
        /// Gets the start of the quarter containing the specified date
        /// </summary>
        public static DateTime GetStartOfQuarter(DateTime date)
        {
            int quarter = GetQuarter(date);
            int firstMonthOfQuarter = (quarter - 1) * 3 + 1;
            return new DateTime(date.Year, firstMonthOfQuarter, 1);
        }

        /// <summary>
        /// Gets a string identifier for a week (e.g., "2024-W01")
        /// </summary>
        public static string GetWeekIdentifier(DateTime date)
        {
            // ISO 8601 week-based year and week number
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            int weekOfYear = cal.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return $"{date.Year}-W{weekOfYear:D2}";
        }

        /// <summary>
        /// Formats a date as a day label (dd/MM/yyyy)
        /// </summary>
        public static string FormatDayLabel(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Formats a week range as a label (e.g., "01/01/2024 - 07/01/2024")
        /// </summary>
        public static string FormatWeekLabel(DateTime weekStart, DateTime weekEnd)
        {
            return $"{FormatDayLabel(weekStart)} - {FormatDayLabel(weekEnd)}";
        }

        /// <summary>
        /// Formats a date as a month label (e.g., "January 2024")
        /// </summary>
        public static string FormatMonthLabel(DateTime date)
        {
            return date.ToString("MMMM yyyy");
        }

        /// <summary>
        /// Formats a date as a quarter label (e.g., "Q1 2024")
        /// </summary>
        public static string FormatQuarterLabel(DateTime date)
        {
            int quarter = GetQuarter(date);
            return $"Q{quarter} {date.Year}";
        }


    }
}