using Microsoft.AspNetCore.Mvc;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }

        private Models.BddContext _bddContext;

        public EmployeeController()
        {
            _bddContext = new Models.BddContext();
        }

        
    }
}
