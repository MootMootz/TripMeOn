using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.BL
{
    public class PropositionService : IPropositionService
    {

        private BddContext _bddContext;

        public PropositionService()
        {
            _bddContext = new BddContext();
        }

        public int CreateAccomodation(string name, string type, int capacity, double price, int partnerId, int destinationId)
        {
            Accomodation accomodation = new Accomodation() { Name = name, Type = type, Capacity = capacity, Price = price, PartnerId = partnerId, DestinationId = destinationId };
            _bddContext.Accomodations.Add(accomodation);
            _bddContext.SaveChanges();
            return accomodation.Id;
        }
        public int CreateRestaurant(string name, string type, double price, int partnerId, int destinationId)
        {
            Restaurant restaurant = new Restaurant() { Name = name, Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId };
            _bddContext.Restaurants.Add(restaurant);
            _bddContext.SaveChanges();
            return restaurant.Id;
        }
        public int CreateTransportation(string type, double price, int partnerId, int destinationId)
        {
            Transportation transportation = new Transportation() { Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId };
            _bddContext.Transportations.Add(transportation);
            _bddContext.SaveChanges();
            return transportation.Id;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public List<Accomodation> GetAllAccomodations()
        {
            return _bddContext.Accomodations.ToList();
        }

        public List<Restaurant> GetAllRestaurant()
        {
            return _bddContext.Restaurants.ToList();
        }

        public List<Transportation> GetAllTransportations()
        {
            return _bddContext.Transportations.ToList();
        }
    }
}
