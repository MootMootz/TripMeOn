using System;
using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.BL.interfaces
{
    public interface IPropositionService : IDisposable
    {
        int CreateAccomodation(string name, string type, int capacity, double price, int partnerId, int destinationId);
        int CreateRestaurant(string name, string type, double price, int partnerId, int destinationId);
        int CreateTransportation(string type, double price, int partnerId, int destinationId);
        List<Accomodation> GetAllAccomodations();
        List<Restaurant> GetAllRestaurant();
        List<Transportation> GetAllTransportations();
    }
}
