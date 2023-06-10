using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.Models.Users
{
    public class Partner
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Accomodation> Accomodations { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<Transportation> Transportations { get; set; }
    }
}
