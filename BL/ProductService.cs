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
        private readonly Models.BddContext _bddContext;

        public ProductService()
        {
            _bddContext = new Models.BddContext();
        }

        /// <summary>
        /// méthode utilisé pour chercher les destinations, et les trier par pays, en regroupant les destinations du même pays pour éviter la répetition
        /// </summary>
        /// <returns>les pays triés</returns>
        public List<Destination> GetDestinations()
        {
            List<Destination> destinations = new List<Destination>();
           
            using (var _bddContext = new Models.BddContext())
            {
                destinations = _bddContext.Destinations.ToList();// original code to retrieve all destinations in-memory LINQ-to-Objects       

            }
            // the query will be performed on the client side instead of being translated to SQL:InvalidOperationException
            destinations = destinations.GroupBy(d => d.Country) // groups the destinations by country
                              .Select(group => group.First())  //selects the first destination from each group
                              .Distinct()
                              .ToList();
            return destinations;
        }


        public List<string> GetDistinctCountries()
        {
            List<string> countries = new List<string>();

            using (var _bddContext = new Models.BddContext())
            {
                countries = _bddContext.Destinations
                    .Select(d => d.Country)
                    .Distinct()
                    .ToList();
            }

            return countries;
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

        /// <summary>
        /// méthode pour récuperer les paquets touristiques et les afficher avec ses atributs
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// méthode re recherche de paquets par iD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TourPackage GetTourPackageById(int id)
        {
            using var dbContext = new Models.BddContext();
            return dbContext.TourPackages
                .Include(tp => tp.Destination)
                    .ThenInclude(d => d.City)
                .Include(tp => tp.Destination)
                    .ThenInclude(d => d.Region)
                .Include(tp => tp.Destination)
                    .ThenInclude(d => d.Country)
                .Include(tp => tp.Theme)
                .Include(tp => tp.TimePeriod)
                .Include(tp => tp.Image)
                .FirstOrDefault(tp => tp.Id == id);
        }

        /// <summary>
        /// méthode de recherche de paquets par mois
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<TourPackage> GetTourPackagesByMonth(int month)
        {
            using var dbContext = new Models.BddContext();
            return dbContext.TourPackages.Include(tp => tp.Destination).Include(tp => tp.Theme).Include(tp => tp.TimePeriod).Include(tp => tp.Image)
                .Where(tp => tp.TimePeriod.StartMonth == month)
                .ToList();
        }

		//Include = retrieve related entities along with the main entity in a single query to optimize performance.
        /// <summary>
        /// Recherche multicritère des paquets touristiques
        /// </summary>
        /// <param name="country">recherche par pays</param>
        /// <param name="themeId">recherche par thème</param>
        /// <param name="month">recherche par mois</param>
        /// <returns>les paquets qui répondent à tous les critères selectionnés</returns>
		public List<TourPackage> SearchByDestinationThemeMonth(string country, int? themeId, int? month)
		{
			using var dbContext = new Models.BddContext();

			var query = dbContext.TourPackages.Include(tp => tp.Destination)
											  .Include(tp => tp.Theme)
											  .Include(tp => tp.TimePeriod)
											  .Include(tp => tp.Image)
											  .AsQueryable();

			if ( country != "All Destinations" && country!=null)
			{
				query = query.Where(tp => tp.Destination.Country == country);
			}

			if (themeId.HasValue)
			{
				query = query.Where(tp => tp.ThemeId == themeId.Value);
			}

			if (month.HasValue)
			{
				query = query.Where(tp => tp.TimePeriod.StartMonth == month.Value);
			}

			return query.ToList(); ;
		}

        /// <summary>
        /// Méthode de création d'un paquet turistique
        /// </summary>
        /// <param name="name">Nom du paquet à afficher comme titre</param>
        /// <param name="country">pays de destination</param>
        /// <param name="themeName">thématique du voyage</param>
        /// <param name="region">region du pays de destination</param>
        /// <param name="city">ville principale</param>
        /// <param name="description">description courte du voyage</param>
        /// <param name="startMonth">à partir de quel mois il est récommandé</param>
        /// <param name="endMonth">jusqu'à quel mois on doit le prendre</param>
        /// <param name="price">prix du paquet complet</param>
        /// <returns></returns>
		public TourPackage CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            using var _bddContext = new Models.BddContext();
            {
                // Vérifie qu'il n'y a pas de destination avec le même pays, dans la même region et dans la même ville pour éviter de dupliquer les informations sur la base de donnés
                Destination destination = _bddContext.Destinations.FirstOrDefault(d =>
                    d.Country == country && d.Region == region && d.City == city);

                // si la destination n'existe pas, on va la créer
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

                // Cherche si le thème existe dans la base de donnés
                Theme theme = _bddContext.Themes.FirstOrDefault(t => t.Name == themeName);

                // si le thème n'existe pas, on le crée
                if (theme == null)
                {
                    theme = new Theme
                    {
                        Name = themeName
                    };
                    _bddContext.Themes.Add(theme);
                }

                // On cherche si la même période du début et fin de disponibilité dans un paquet existe déjà
                TimePeriod timePeriod = _bddContext.TimePeriods.Include(tp => tp.TourPackages).SingleOrDefault(tp => tp.StartMonth == startMonth && tp.EndMonth == endMonth);

                // si la période n'existe pas, on va la créér
                if (timePeriod == null)
                {
                    timePeriod = new TimePeriod
                    {
                        StartMonth = startMonth,
                        EndMonth = endMonth
                    };
                    _bddContext.TimePeriods.Add(timePeriod);
                }
                //on instancie un nouveau TourPackage
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
        /// <summary>
        /// méthode pour modifier un paquet
        /// </summary>

        public TourPackage ModifyPackage(int packageId, string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            using var _bddContext = new Models.BddContext();
            {
                // Récuperer le paquet de la base de donnés
                TourPackage package = _bddContext.TourPackages
                    .Include(tp => tp.Destination)
                    .Include(tp => tp.Theme)
                    .Include(tp => tp.TimePeriod)
                    .FirstOrDefault(tp => tp.Id == packageId);

                if (package != null)
                {
                    // si le paquet existe, on modifie ses attributs
                    package.Name = name;
                    package.Description = description;
                    package.Price = price;

                    // on choisit ou on crée une autre destination si on veut
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
                    // on choisit ou on crée un thème pour le paquet
                    Theme theme = _bddContext.Themes.FirstOrDefault(t => t.Name == themeName);

                    if (theme == null)
                    {
                        theme = new Theme
                        {
                            Name = themeName
                        };
                        _bddContext.Themes.Add(theme);
                    }
                    // on regarde si la même période existe, sinon on choisit une autre
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
        /// <summary>
        /// Méthode pour effacer un paquet
        /// </summary>
        /// <param name="packageId">on cherche le paquet par son id</param>
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

    }
}
