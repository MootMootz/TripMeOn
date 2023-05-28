using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TripMeOn.Models.Products;

namespace TripMeOn.Helper
{

    public class TimePeriod
    {
        [Key]
        public int Id { get; set; }
        public int StartMonth { get; set; }
        public int EndMonth { get; set; }
       

        //int startMonth = timeSpan.StartMonth;
        //int endMonth = timeSpan.EndMonth;

        //string startMonthName = DateTimeHelper.GetMonthName(startMonth);
        //string endMonthName = DateTimeHelper.GetMonthName(endMonth);

    }
}
