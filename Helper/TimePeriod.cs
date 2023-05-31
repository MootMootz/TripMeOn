using System;
using System.Collections.Generic;
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
        public ICollection<TourPackage> TourPackages { get; set; }

    }
}