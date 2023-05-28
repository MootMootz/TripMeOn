using System.Globalization;

namespace TripMeOn.Helper
{
    public class DateTimeHelper
    {
        public static string GetMonthName(int month)
        {
            if (month >= 1 && month <= 12)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            }

            // Handle invalid month values
            return string.Empty;
        }
        public static string GetMonthNameRange(int startMonth, int endMonth)
        {
            if (startMonth == endMonth)
            {
                return GetMonthName(startMonth);
            }
            else
            {
                string startMonthName = GetMonthName(startMonth);
                string endMonthName = GetMonthName(endMonth);
                return $"{startMonthName} - {endMonthName}";
            }
        }

    }
}
