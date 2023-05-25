using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TripMeOn.Models.Products
{
    public class TourPackage
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }             
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public double Price { get; set; }

    }
}
