using System;
using System.Collections.Generic;
using TripMeOn.Models.Products;

namespace TripMeOn.BL.interfaces
{
    /// <summary>
    /// L'interface IProductService récupère les paquets et les fonctions pour pouvoir les gérer depuis l'IHM
    /// </summary>
    public interface IProductService:IDisposable
    {

		// void CreateProduct();
		List<Destination> GetDestinations();
		List<Theme> GetThemes();
		List<TourPackage> GetTourPackages();
        List<TourPackage> SearchByDestinationThemeMonth(string country, int? themeId, int? month);
        TourPackage CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price);
        TourPackage ModifyPackage(int packageId, string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price);
        void RemovePackage(int packageId);
        TourPackage GetTourPackageById(int id);
        List<TourPackage> GetTourPackagesByMonth(int month);
        List<string> GetDistinctCountries();




    }
}
