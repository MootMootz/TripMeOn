using System;
using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.BL.interfaces
{
    public interface IPropositionService : IDisposable
    {

        int CreateAccomodation(string name, string type, int capacity, double price, int partnerId, int destinationId, DateTime startdate, DateTime enddate);
        int CreateRestaurant(string name, string type, double price, int partnerId, int destinationId, DateTime startdate, DateTime enddate);
        int CreateTransportation(string type, double price, int partnerId, int destinationId, DateTime startdate, DateTime enddate);
        void DeleteRestaurant(Restaurant restaurant);
        void DeleteAccomodation(Accomodation accomodation);
        void DeleteTransportation(Transportation transportation);

        List<Accomodation> GetAllAccomodations();
        List<Restaurant> GetAllRestaurants();
        List<Transportation> GetAllTransportations();
        List<Accomodation> GetAccomodationsByPartnerId(int partnerId);
        List<Restaurant> GetRestaurantsByPartnerId(int partnerId);
        List<Transportation> GetTransportationsByPartnerId(int partnerId);
        List<Notification> GetAllNotifications();
    }
}
