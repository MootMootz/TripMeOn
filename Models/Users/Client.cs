using System.Collections.Generic;
using TripMeOn.Models.Order;

namespace TripMeOn.Models.Users
{
    public class Client
    {
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientType { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
