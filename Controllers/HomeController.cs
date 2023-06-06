using Microsoft.AspNetCore.Mvc;
using TripMeOn.BL.interfaces;
using TripMeOn.BL;
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

        public IActionResult ShowRestaurantList()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                //int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Restaurants = propositionService.GetAllRestaurants();
                return View(viewModel);
            }
        }

        public IActionResult ShowAccomodationList()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                //int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Accomodations = propositionService.GetAllAccomodations();
                return View(viewModel);
            }
        }

        public IActionResult ShowTransportationList()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                //int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Transportations = propositionService.GetAllTransportations();
                return View(viewModel);
            }
        }
    }
}


