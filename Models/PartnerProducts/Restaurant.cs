using System.ComponentModel.DataAnnotations;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;

namespace TripMeOn.Models.PartnerProducts
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int PartnerId { get; set; } // foreign key for Partner
        public virtual Partner Partner { get; set; } // navigation property

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
    }
}

