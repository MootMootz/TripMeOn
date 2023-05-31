using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripMeOn.Models.Order
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
