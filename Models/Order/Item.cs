using System.ComponentModel.DataAnnotations;
using TripMeOn.Models.Products;

namespace TripMeOn.Models.Order
{
    public class Item
    {
        [Key] 
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TourPackageId { get; set; }
        public TourPackage TourPackage { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
