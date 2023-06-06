using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly BddContext _bddContext;

        public PropositionService()
        {
            _bddContext = new BddContext();
        }
        public List<Accomodation> GetAllAccomodations()
        {
            return _bddContext.Accomodations.Include(a => a.Image).ToList();
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _bddContext.Restaurants.Include(r => r.Image).ToList();
        }

        public List<Transportation> GetAllTransportations()
        {
            return _bddContext.Transportations.Include(t => t.Image).ToList();
        }

        public List<Accomodation> GetAccomodationsByPartnerId(int partnerId)
        {
            return _bddContext.Accomodations.Where(a => a.PartnerId == partnerId).ToList();
        }

        public List<Restaurant> GetRestaurantsByPartnerId(int partnerId)
        {
            return _bddContext.Restaurants.Where(r => r.PartnerId == partnerId).ToList();
        }

        public List<Transportation> GetTransportationsByPartnerId(int partnerId)
        {
            return _bddContext.Transportations.Where(t => t.PartnerId == partnerId).ToList();
        }
        public List<Notification> GetAllNotifications()
        {
            return _bddContext.Notifications.OrderByDescending(n => n.CreatedAt).ToList();
        }

        public int CreateAccomodation(string name, string type, int capacity, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate)
        {
            Accomodation accomodation = new Accomodation() { Name = name, Type = type, Capacity = capacity, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate };
            _bddContext.Accomodations.Add(accomodation);
            Notification notification = new Notification() { Message = $"A new accomodation has been created by partner {partnerId}.", CreatedAt = DateTime.Now };
            _bddContext.Notifications.Add(notification);
            _bddContext.SaveChanges();
            return accomodation.Id;
        }
        public int CreateRestaurant(string name, string type, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate, string description)
        {
            Restaurant restaurant = new Restaurant() { Name = name, Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate, Description = description };
            _bddContext.Restaurants.Add(restaurant);
            Notification notification = new Notification() { Message = $"A new restaurant has been created by partner {partnerId}.", CreatedAt = DateTime.Now };
            _bddContext.Notifications.Add(notification);
            _bddContext.SaveChanges();
            return restaurant.Id;
        }
        public int CreateTransportation(string type, double price, int partnerId, int destinationId, DateTime startDate, DateTime endDate, string description)
        {
            Transportation transportation = new Transportation() { Type = type, Price = price, PartnerId = partnerId, DestinationId = destinationId, StartDate = startDate, EndDate = endDate, Description = description };
            _bddContext.Transportations.Add(transportation);
            Notification notification = new Notification() { Message = $"A new transportation has been created by partner {partnerId}.", CreatedAt = DateTime.Now };
            _bddContext.Notifications.Add(notification);
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
    }
}
