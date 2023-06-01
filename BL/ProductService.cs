using System.Collections.Generic;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using TripMeOn.Helper;
using Microsoft.EntityFrameworkCore.Internal;

namespace TripMeOn.BL
{
    public class ProductService : IProductService
    {
        private Models.BddContext _bddContext;

        public ProductService()
        {
            _bddContext = new Models.BddContext();
        }
        public List<Destination> GetDestinations()
        {
            List<Destination> destinations = new List<Destination>();
            //Debugged : Group the destinations from the same country and display the first one only to aviod duplicate entries
            using (var _bddContext = new Models.BddContext())
            {
                destinations = _bddContext.Destinations.ToList();// original code to retrieve all destinations in-memory LINQ-to-Objects       

			}
			// the query will be performed on the client side instead of being translated to SQL:InvalidOperationException
			destinations = destinations.GroupBy(d => d.Country) // groups the destinations by country
							  .Select(group => group.First())  //selects the first destination from each group
							  .ToList();
			return destinations;
        }
        public List<Theme> GetThemes()
        {
            List<Theme> themes = new List<Theme>();

            using (var dbContext = new Models.BddContext())
            {
                themes = dbContext.Themes.ToList();
            }

            return themes;
        }
        public List<TourPackage> GetTourPackages()
        {
            List<TourPackage> tourPackages = new List<TourPackage>();

            using (var dbContext = new Models.BddContext())
            {
                tourPackages = dbContext.TourPackages
                    .Include(tp => tp.Destination)
                    .Include(tp => tp.Theme)
                    .Include(tp => tp.TimePeriod)
                    .Include(tp => tp.Image)
                    .ToList();
            }

            return tourPackages;
        }

        public TourPackage GetTourPackageById(int id)
        {
            using (var dbContext = new Models.BddContext())
            {
                return dbContext.TourPackages
                    .Include(tp => tp.Destination)
                    .Include(tp => tp.Theme)
                    .Include(tp => tp.TimePeriod)
                    .Include(tp => tp.Image)
                    .FirstOrDefault(tp => tp.Id == id);
            }
        }
        public List<TourPackage> GetTourPackagesByMonth(int month)
        {
            using (var dbContext = new Models.BddContext())
            {
                return dbContext.TourPackages.Include(tp => tp.Destination).Include(tp => tp.Theme).Include(tp => tp.TimePeriod).Include(tp => tp.Image)
                    .Where(tp => tp.TimePeriod.StartMonth == month)
                    .ToList();
            }
        }

        //Include = retrieve related entities along with the main entity in a single query to optimize performance.
        public List<TourPackage> SearchByDestinationAndTheme(int destinationId, int themeId)
        {
            using (var _bddContext = new Models.BddContext())
            {
                var packages = _bddContext.TourPackages.Include(tp => tp.Destination)
                                                       .Include(tp => tp.Theme) 
                                                       .Include(tp => tp.Image)
                                                       .Where(tp => tp.DestinationId == destinationId && tp.ThemeId == themeId)
                                                       .ToList();

                return packages;
            }
        }
        public TourPackage CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            using (var _bddContext = new Models.BddContext())
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
            using (var _bddContext = new Models.BddContext())
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
            using (var _bddContext = new Models.BddContext())
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
