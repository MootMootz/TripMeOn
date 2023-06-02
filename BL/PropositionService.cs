using System;
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

        public List<Accomodation> GetAllAccomodations()
        {
            return _bddContext.Accomodations.ToList();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _bddContext.Restaurants.ToList();
        }

        public List<Transportation> GetAllTransportations()
        {
            return _bddContext.Transportations.ToList();
        }

        public int CreateAccomodation(string name, string type, int capacity, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate)
        {
            Accomodation accomodation = new Accomodation() { Name = name, Type = type, Capacity = capacity, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate };
            _bddContext.Accomodations.Add(accomodation);
            _bddContext.SaveChanges();
            return accomodation.Id;
        }
        public int CreateRestaurant(string name, string type, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate)
        {
            Restaurant restaurant = new Restaurant() { Name = name, Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate };
            _bddContext.Restaurants.Add(restaurant);
            _bddContext.SaveChanges();
            return restaurant.Id;
        }
        public int CreateTransportation(string type, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate)
        {
            Transportation transportation = new Transportation() { Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate };
            _bddContext.Transportations.Add(transportation);
            _bddContext.SaveChanges();
            return transportation.Id;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void ModifyAccomodation(Accomodation accomodation) // applique les modifications sur accomodation et enregistre ces modifications dans la base de données
        {
            _bddContext.Accomodations.Update(accomodation);
            _bddContext.SaveChanges();
        }

        public void ModifyRestaurant(Restaurant restaurant) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Restaurants.Update(restaurant);
            _bddContext.SaveChanges();
        }
        public void ModifyTransportation(Transportation transportation) // applique les modifications sur transportation et enregistre ces modifications dans la base de données
        {
            _bddContext.Transportations.Update(transportation);
            _bddContext.SaveChanges();
        }

        public void DeleteRestaurant(Restaurant restaurant) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Restaurants.Remove(restaurant);
            _bddContext.SaveChanges();
        }
        public void DeleteAccomodation(Accomodation accomodation) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Accomodations.Remove(accomodation);
            _bddContext.SaveChanges();
        }
        public void DeleteTransportation(Transportation transportation) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Transportations.Remove(transportation);
            _bddContext.SaveChanges();
        }


        //public void ModifyAccomodation(int id, string name, string type, int capacity, double price, int partnerId, int destinationId)
        //{
        //    Accomodation accomodation = _bddContext.Accomodations.Find(id);
        //    if (accomodation != null)
        //    {
        //        accomodation.Name = name; // on met à jour
        //        accomodation.Type = type;
        //        accomodation.Price = price;
        //        accomodation.PartnerId = partnerId;
        //        accomodation.DestinationId = destinationId;
        //        accomodation.Capacity = capacity;
        //        _bddContext.SaveChanges(); // on enregistre les changements
        //    }
        //}


        //public void ModifyRestaurant(int id, string name, string type, double price, int partnerId, int destinationId)
        //{
        //    Restaurant restaurant = _bddContext.Restaurants.Find(id);
        //    if (restaurant != null)
        //    {
        //        restaurant.Name = name; // on met à jour
        //        restaurant.Type = type;
        //        restaurant.Price = price;
        //        restaurant.PartnerId = partnerId;
        //        restaurant.DestinationId = destinationId;
        //        _bddContext.SaveChanges(); // on enregistre les changements
        //    }
        //}




        //public void ModifyTransportation(int id, string type, double price, int partnerId, int destinationId)
        //{
        //    Transportation transportation = _bddContext.Transportations.Find(id);
        //    if (transportation != null)
        //    {
        //        transportation.Type = type; // on met à jour
        //        transportation.Price = price;
        //        transportation.PartnerId = partnerId;
        //        transportation.DestinationId = destinationId;
        //        _bddContext.SaveChanges(); // on enregistre les changements
        //    }
        //}
    }
}
