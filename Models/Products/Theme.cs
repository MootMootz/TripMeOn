using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripMeOn.Models.Products
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TourPackage> TourPackages { get; set; }
    }
}
