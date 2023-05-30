using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripMeOn.Helper;

namespace TripMeOn.Models.Products
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }        
        public virtual ICollection<TourPackage> TourPackages { get; set; }
    }
}
