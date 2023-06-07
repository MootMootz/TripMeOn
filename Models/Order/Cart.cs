using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TripMeOn.Models.Users;

namespace TripMeOn.Models.Order
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Item> Items { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public bool IsRefunded { get; set; } // Add this property to represent refund status
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
