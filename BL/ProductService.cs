using System.Collections.Generic;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace TripMeOn.BL
{
    public class ProductService : IProductService
    {
        private BddContext _bddContext;

        public ProductService()
        {
            _bddContext = new BddContext();
        }
       

        public List<Destination> GetDestinations()
        {
            List<Destination> destinations = new List<Destination>();

            using (var _bddContext = new BddContext())
            {
                destinations = _bddContext.Destinations.ToList();
            }

            return destinations;
		}


        public List<Theme> GetThemes()
        {
            List<Theme> themes = new List<Theme>();

            using (var dbContext = new BddContext())
            {
                themes = dbContext.Themes.ToList();
            }

            return themes;
        }

        public List<TourPackage> GetTourPackages()
        {
            List<TourPackage> tourPackages = new List<TourPackage>();

            using (var dbContext = new BddContext())
            {
                tourPackages = dbContext.TourPackages
                    .Include(tp => tp.Destination)
                    .Include(tp => tp.Theme)
                     .Include(tp => tp.Destination.TimePeriod)
                    .ToList();
            }

            return tourPackages;
        }


        public List<TourPackage> SearchByDestinationAndTheme(int destinationId, int themeId)
		{// when there is FK 
			List<TourPackage> searchResults = _bddContext.TourPackages.Include(t => t.Destination).Include(t=>t.Theme)
			.Where(tp => tp.Destination.Id== destinationId &&
					  tp.Theme.Id== themeId).ToList();

			return searchResults;
		}

        public int CreatePackage(string name, int destinationId, int themeId,string description, double price )
        {
            TourPackage tourPackage = new TourPackage { Name=name, DestinationId= destinationId, ThemeId = themeId, Description = description,Price=price};
            _bddContext.TourPackages.Add(tourPackage);
            _bddContext.SaveChanges();

            return tourPackage.Id;
        }
        //public  void RemovePackage(TourPackage tourPackage)
        //{
        //	tourPackages.Remove(tourPackage);
        //}

        //public  List<TourPackage> SearchByPackageId(int id)
        //{
        //	List<TourPackage> searchResults = tourPackages.Where(tp => tp.Id == id).ToList();
        //	return searchResults;
        //}

        //public  List<TourPackage> SearchByDestination(string keyword)
        //{
        //	List<TourPackage> searchResults = TourPackageList.Where(tp => tp.Destination.Country.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        //	return searchResults;
        //}

        //public  List<TourPackage> SearchByTheme(string keyword)
        //{
        //	List<TourPackage> searchResults = TourPackageList.Where(tp => tp.Theme.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        //	return searchResults;
        //}


        public void Dispose()
		{
			_bddContext.Dispose();
		}
		// Create

		// Modify

		// Delete

		// etc...
	}
}
