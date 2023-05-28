using System.Collections.Generic;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using TripMeOn.Helper;

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
                    .Include(tp => tp.TimePeriod)
                    .ToList();
            }

            return tourPackages;
        }
        //Include = retrieve related entities along with the main entity in a single query to optimize performance.
        public List<TourPackage> SearchByDestinationAndTheme(int destinationId, int themeId)
        {
            using (var _bddContext = new BddContext())
            {
                var packages = _bddContext.TourPackages.Include(tp => tp.Destination)
                                                       .Include(tp => tp.Theme)                                                      
                                                       .Where(tp => tp.DestinationId == destinationId && tp.ThemeId == themeId)
                                                       .ToList();

                return packages;
            }
        }
        public TourPackage CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            using (var _bddContext = new BddContext())
            {
                // Check if a destination with the same country, region, and city exists in the database to avoid duplicate
                Destination destination = _bddContext.Destinations.FirstOrDefault(d =>
                    d.Country == country && d.Region == region && d.City == city);

                // If the destination doesn't exist, create a new one
                if (destination == null)
                {
                    destination = new Destination
                    {
                        Country = country,
                        Region = region,
                        City = city
                    };
                    _bddContext.Destinations.Add(destination);
                }

                // Find and Check if a theme with the same name exists in the database
                Theme theme = _bddContext.Themes.FirstOrDefault(t => t.Name == themeName);

                // If the theme doesn't exist, create a new one
                if (theme == null)
                {
                    theme = new Theme
                    {
                        Name = themeName
                    };
                    _bddContext.Themes.Add(theme);
                }

                // Retrieve the TimePeriod entity with the specified start month and end month, including eager loading
                TimePeriod timePeriod = _bddContext.TimePeriods.Include(tp => tp.TourPackages).SingleOrDefault(tp => tp.StartMonth == startMonth && tp.EndMonth == endMonth);

                // If the time period doesn't exist, create a new one
                if (timePeriod == null)
                {
                    timePeriod = new TimePeriod
                    {
                        StartMonth = startMonth,
                        EndMonth = endMonth
                    };
                    _bddContext.TimePeriods.Add(timePeriod);
                }
                // Create a new TourPackage object
                TourPackage package = new TourPackage
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Destination = destination,
                    Theme = theme,
                    TimePeriod = timePeriod
                };

                _bddContext.TourPackages.Add(package);
                _bddContext.SaveChanges();

                return package;
            }
        }

        public TourPackage ModifyPackage(int packageId, string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            using (var _bddContext = new BddContext())
            {
                // Retrieve the existing TourPackage from the database
                TourPackage package = _bddContext.TourPackages
                    .Include(tp => tp.Destination)
                    .Include(tp => tp.Theme)
                    .Include(tp => tp.TimePeriod)
                    .FirstOrDefault(tp => tp.Id == packageId);

                if (package != null)
                {
                    // Update the properties of the TourPackage
                    package.Name = name;
                    package.Description = description;
                    package.Price = price;

                    // Update or create the Destination based on the provided values
                    Destination destination = _bddContext.Destinations
                        .FirstOrDefault(d => d.Country == country && d.Region == region && d.City == city);

                    if (destination == null)
                    {
                        destination = new Destination
                        {
                            Country = country,
                            Region = region,
                            City = city
                        };
                        _bddContext.Destinations.Add(destination);
                    }
                    // Update or create the Theme based on the provided themeName
                    Theme theme = _bddContext.Themes.FirstOrDefault(t => t.Name == themeName);

                    if (theme == null)
                    {
                        theme = new Theme
                        {
                            Name = themeName
                        };
                        _bddContext.Themes.Add(theme);
                    }
                    // Check if a TimePeriod with the same startMonth and endMonth exists
                    TimePeriod timePeriod = _bddContext.TimePeriods
                        .FirstOrDefault(tp => tp.StartMonth == startMonth && tp.EndMonth == endMonth);

                    if (timePeriod == null)
                    {
                        timePeriod = new TimePeriod
                        {
                            StartMonth = startMonth,
                            EndMonth = endMonth
                        };
                        _bddContext.TimePeriods.Add(timePeriod);
                    }
                    package.Destination = destination;
                    package.Theme = theme;
                    package.TimePeriod = timePeriod;

                    _bddContext.SaveChanges();
                }
                return package;
            }
        }
        public void RemovePackage(int packageId)
        {
            using (var _bddContext = new BddContext())
            {
                var package = _bddContext.TourPackages.FirstOrDefault(p => p.Id == packageId);
                if (package != null)
                {
                    _bddContext.TourPackages.Remove(package);
                    _bddContext.SaveChanges();
                }
            }
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

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

    }
}
