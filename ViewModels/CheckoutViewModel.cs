using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripMeOn.Models.Order;
using TripMeOn.Models.Users;

namespace TripMeOn.ViewModels
{
    public class CheckoutViewModel
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Item> Items { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public virtual Client Client { get; set; }
        public bool IsRefunded { get; set; } // Add this property to represent refund status
        public int TourPackageId { get; set; }
        public int Quantity { get; set; }
    }
}
