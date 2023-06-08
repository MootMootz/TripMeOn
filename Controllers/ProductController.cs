using Microsoft.AspNetCore.Mvc;
using TripMeOn.BL.interfaces;
using TripMeOn.Models.Products;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TripMeOn.Controllers
{
    /// <summary>
    /// Class controller pour faire la gestion de paquets touristiques
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public IActionResult SearchPackage(string country, int theme, int month)
        {


            var searchResults = _productService.SearchByDestinationThemeMonth(
                country,
                theme == 0 ? (int?)null : theme,
                month == 0 ? (int?)null : month);

            return View("SearchBoxPackage", searchResults);
        }
        public ActionResult PackageListUniqueCountry()
        {
            var productService = new TripMeOn.BL.ProductService();
            var distinctCountries = productService.GetDistinctCountries();
            var countryList = new SelectList(distinctCountries);
            ViewBag.CountryList = countryList;

            // Other code logic for your index action

            return View();
        }
        public IActionResult PackageList()
        {
            var packages = _productService.GetTourPackages();

            return View("PackageList", packages);
        }
        [HttpPost]
        public IActionResult CreatePackage(string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {

            var newPackage = _productService.CreatePackage(name, country, themeName, region, city, description, startMonth, endMonth, price);

            var packages = _productService.GetTourPackages();

            return View("PackageList", packages);
        }
        //GetTourPackages return a collection , had to use using System.Linq;

        [HttpGet]
        public IActionResult ModifyPackage(int packageId)
        {
            var package = _productService.GetTourPackages()
                .FirstOrDefault(p => p.Id == packageId);

            if (package != null)
            {
                return View("ModifyPackage", package);
            }

            // Handle the case where the package is not found
            return RedirectToAction("PackageList");
        }


        [HttpPost]
        public IActionResult ModifyPackage(int packageId, string name, string country, string themeName, string region, string city, string description, int startMonth, int endMonth, double price)
        {
            var modifiedPackage = _productService.ModifyPackage(packageId, name, country, themeName, region, city, description, startMonth, endMonth, price);

            if (modifiedPackage != null)
            {
                return RedirectToAction("PackageList");
            }

            // Handle the case where the modification fails
            TourPackage originalPackage = _productService.GetTourPackages()
                .FirstOrDefault(p => p.Id == packageId);

            return View("ModifyPackage", originalPackage);
        }

        [HttpGet]
        public IActionResult ShowRemovePackage(int packageId)
        {
            var package = _productService.GetTourPackages().FirstOrDefault(p => p.Id == packageId);

            if (package != null)
            {
                return View("RemovePackage", package);
            }

            // Handle the case where the package is not found
            return RedirectToAction("PackageList");
        }

        [HttpPost]
        public IActionResult RemovePackage(int packageId)
        {
            var package = _productService.GetTourPackages().FirstOrDefault(p => p.Id == packageId);

            if (package != null)
            {
                _productService.RemovePackage(packageId);
            }

            return RedirectToAction("PackageList");
        }

        public IActionResult Details(int id)
        {
            var tourPackage = _productService.GetTourPackages().FirstOrDefault(tp => tp.Id == id);

            if (tourPackage == null)
            {
                return NotFound();
            }

            //string viewName = $"/Views/Package/Details_{id}.cshtml";
            return View(tourPackage);
        }

        //public IActionResult Catalog()
        //{
        //    return View();
        //}
    }

}

