using System;
using System.Collections.Generic;
using TripMeOn.Models.Products;

namespace TripMeOn.BL.interfaces
{
    public interface IProductService:IDisposable
    {

		// void CreateProduct();
		List<Destination> GetDestinations();
		List<Theme> GetThemes();
		List<TourPackage> GetTourPackages();
		List<TourPackage> SearchByDestinationAndTheme(int destinationId, int themeId);
        TourPackage CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price);
        TourPackage ModifyPackage(int packageId, string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price);
        void RemovePackage(int packageId);
       
        //List<TourPackage> SearchByPackageId(int id);
        //List<TourPackage> SearchByDestination(string keyword);
        //List<TourPackage> SearchByTheme(string keyword);
     


      
    }
}
