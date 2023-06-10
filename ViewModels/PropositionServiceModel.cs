using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Users;

namespace TripMeOn.ViewModels
{
    public class PropositionServiceModel
    {
        public List<Accomodation> Accomodations { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<Transportation> Transportations { get; set; }

        public List<Partner> Partners { get; set; }
    }
}
