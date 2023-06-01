using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripMeOn.Models.Products
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
    }
}
