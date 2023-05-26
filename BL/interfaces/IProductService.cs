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
		//void AddPackage(TourPackage tourPackage);
		//void RemovePackage(TourPackage tourPackage);
		//List<TourPackage> SearchByPackageId(int id);
		//List<TourPackage> SearchByDestination(string keyword);
		//List<TourPackage> SearchByTheme(string keyword);
		// void ModifyProduct();


		//etc...
	}
}
