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
    }
}
