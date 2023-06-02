using Microsoft.AspNetCore.Mvc;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomePage()
        {
            NavigationViewModel navigationViewModel = new NavigationViewModel
            {
                Logo = "/images/logo.png",
                AboutUs = "About Us",
                Destination = "Destinations",
                HolidayFinder = "Holiday Finder",
                Theme = "Theme",
                Connect = "Connect"
            };

            return View("HomePage", navigationViewModel);
        }


    }
}


