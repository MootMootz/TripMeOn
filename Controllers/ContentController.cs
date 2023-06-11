using Microsoft.AspNetCore.Mvc;

namespace TripMeOn.Controllers
{
	public class ContentController : Controller
	{
		public IActionResult AboutUs()
		{
			return View();
		}

        public IActionResult InstaTips()
        {
            return View();
        }

        public IActionResult BecomePartner()
        {
            return View();
        }
    }
}
