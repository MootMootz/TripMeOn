using System;
using System.Collections.Generic;
using TripMeOn.Models.Order;

namespace TripMeOn.ViewModels
{
    public class OrderViewModel
    {
        public int CartId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public List<ItemViewModel> Items { get; set; }
        public bool IsRefunded { get; set; }    
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public List<Cart> Carts { get; set; }
    

    }
}
