using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        private Models.BddContext _bddContext;

        public ClientController()
        {
            _bddContext = new Models.BddContext();
        }
        [HttpGet]
        public IActionResult ClientForm()
        {
            var viewModel = new ClientViewModel();
            return View("AddClientForm");
        }

        [HttpPost]
        public IActionResult SubmitClientForm(ClientViewModel model)
        {

            using (var dbContext = new Models.BddContext())
            {

                var client = new Client
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    ClientType = model.ClientType
                };

                dbContext.Clients.Add(client);
                dbContext.SaveChanges();

                return base.RedirectToAction("SignUpConfirmation");
            }
        }

    }
}
