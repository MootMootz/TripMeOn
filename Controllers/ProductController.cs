using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
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

	
		public IActionResult Search(int destination, int theme)
		{
			var searchResults = _productService.SearchByDestinationAndTheme(destination, theme);
			return View("SearchBoxPackage", searchResults);
        }
       
        public IActionResult PackageList()
        {
            var packages = _productService.GetTourPackages();         

            return View("PackageList", packages);
        }
    }

}

