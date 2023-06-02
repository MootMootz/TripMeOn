using Microsoft.AspNetCore.Mvc;
using TripMeOn.BL;
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
					Nickname = model.Nickname,
					Email = model.Email,
                    Password = UserService.EncodeMD5(model.Password),
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    ClientType = model.ClientType
                };

                dbContext.Clients.Add(client);
                dbContext.SaveChanges();
				
				return View("SignUpConfirmation");
            }
        }
        public IActionResult SignUpConfirmation()
        {
            return View();
        }

    }
}
