using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.ViewModels
{
    public class PropositionService
    {
        public List<Accomodation> Accomodations { get; set; }
        public List<Restaurant> Restaurants { get; set; }
        public List<Transportation> Transportations { get; set; }
    }
}
