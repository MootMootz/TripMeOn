using System;
using System.Collections.Generic;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Products;

namespace TripMeOn.BL.interfaces
{
    /// <summary>
    /// Comme l'interface ProductService gère les paquets, cette interface récupère les services individuels et ses fonctions pour pouvoir les utiliser dans l'IHM
    /// </summary>
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
        List<Accomodation> SearchAccomodationByDestinationMonth(int? destinationId, int? month);
        List<Restaurant> SearchRestaurantByDestinationMonth(int? destinationId, int? month);
        List<Transportation> SearchTransportationByDestinationMonth(int? destinationId, int? month);

        List<object> SearchByServiceTypeDestinationMonth(string serviceType, int? destinationId, int? month);


    }
}
