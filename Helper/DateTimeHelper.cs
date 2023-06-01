using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
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
        public static List<SelectListItem> GetMonths()
        {
            var months = new List<SelectListItem>
        {
            new SelectListItem { Value = "01", Text = "January" },
            new SelectListItem { Value = "02", Text = "February" },
            new SelectListItem { Value = "03", Text = "March" },
            new SelectListItem { Value = "04", Text = "April" },
            new SelectListItem { Value = "05", Text = "May" },
            new SelectListItem { Value = "06", Text = "June" },
            new SelectListItem { Value = "07", Text = "July" },
            new SelectListItem { Value = "08", Text = "August" },
            new SelectListItem { Value = "09", Text = "September" },
            new SelectListItem { Value = "10", Text = "October" },
            new SelectListItem { Value = "11", Text = "November" },
            new SelectListItem { Value = "12", Text = "December" },           
        };

            return months;
        }

        public static List<SelectListItem> GetYears()
        {
            var years = new List<SelectListItem>();

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Add the current year and the next 10 years to the list
            for (int i = 0; i < 10; i++)
            {
                years.Add(new SelectListItem { Value = currentYear.ToString(), Text = currentYear.ToString() });
                currentYear++;
            }

            return years;
        }
    }
}

