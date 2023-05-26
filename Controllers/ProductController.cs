using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TripMeOn.BL.interfaces;
using TripMeOn.Models.Products;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

	

		//public IActionResult Search()
		//{
		//	var model = new NavigationSearchBox();
		//	ViewBag.Destinations = _productService.GetDestinations();
		//	ViewBag.Themes = _productService.GetThemes();
		//	return View(model);
		//}

	
		public IActionResult Search(int destination, int theme)
		{
			var searchResults = _productService.SearchByDestinationAndTheme(destination, theme);
			return View("SearchBoxPackage", searchResults);
		}



	}
}
