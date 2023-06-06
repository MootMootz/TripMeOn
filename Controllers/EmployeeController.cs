using Microsoft.AspNetCore.Mvc;

using TripMeOn.BL.interfaces;

using TripMeOn.BL;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;
using System.Linq;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.Controllers
{
    public class EmployeeController : Controller
    {
        private Models.BddContext _bddContext;
        private readonly IPropositionService _propositionService;

        public EmployeeController(IPropositionService propositionService)
        {
            _bddContext = new Models.BddContext();
            _propositionService = propositionService;
        }
        public IActionResult Notifications()
        {
            var notifications = _propositionService.GetAllNotifications();
            return View(notifications);
        }

        public IActionResult IndexAdmin()
        {
            return View();
        }

        public IActionResult ManageAccounts()
        {
            return View();
        }
        public IActionResult RefundRequest()
        {
            return View();
        }

        public IActionResult ListePartner()
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                ManageAccountModel viewModel = new ManageAccountModel();
                viewModel.Partners = manageAccount.GetAllPartners();
                return View(viewModel);
            }
        }
        public IActionResult ListeClient()
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                ManageAccountModel viewModel = new ManageAccountModel();
                viewModel.Clients = manageAccount.GetAllClients();
                return View(viewModel);
            }
        }

        public IActionResult ModifyPartner(int id) // afficher la vue de modification de l'accomodation
        {
            if (id != 0)
            {
                using (IManageAccount manageAccount = new ManageAccount())
                {
                    Partner partner = manageAccount.GetAllPartners().Where(r => r.Id == id).FirstOrDefault();
                    if (partner == null)
                    {
                        return View("Error");
                    }
                    return View(partner);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifyPartner(Partner partner) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(partner);
            if (partner.Id != 0)
            {
                using (ManageAccount manageAccount = new ManageAccount())
                {
                    manageAccount.ModifyPartner(partner);
                    TempData["SuccessMessage"] = "The partner has been modified";
                    return RedirectToAction("ListePartner");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult ModifyClient(int id) // afficher la vue de modification de l'accomodation
        {
            if (id != 0)
            {
                using (IManageAccount manageAccount = new ManageAccount())
                {
                    Client client = manageAccount.GetAllClients().Where(r => r.Id == id).FirstOrDefault();
                    if (client == null)
                    {
                        return View("Error");
                    }
                    return View(client);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifyClient(Client client) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(client);
            if (client.Id != 0)
            {
                using (ManageAccount manageAccount = new ManageAccount())
                {
                    manageAccount.ModifyClient(client);
                    TempData["SuccessMessage"] = "The client has been modified";
                    return RedirectToAction("ListeClient");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult DeletePartner(int id)
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                Partner partner = manageAccount.GetAllPartners().Where(r => r.Id == id).FirstOrDefault();
                manageAccount.DeletePartner(partner);
                return RedirectToAction("ListePartner");
            }
        }

        public IActionResult DeleteClient(int id)
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                Client client = manageAccount.GetAllClients().Where(r => r.Id == id).FirstOrDefault();
                manageAccount.DeleteClient(client);
                return RedirectToAction("ListeClient");
            }
        }
        [HttpGet]
        public IActionResult AddEmployeeForm()
        {
            var viewModel = new EmployeeViewModel();
            return View("AddEmployeeForm");
        }


        [HttpPost]
        public IActionResult SubmitEmployeeForm(EmployeeViewModel model)
        {

            using (var dbContext = new Models.BddContext())
            {
                var employee = new Employee
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Nickname = model.Nickname,
                    Email = model.Email,
                    Password = UserService.EncodeMD5(model.Password),
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role
                };

                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                return View("../Employee/IndexAdmin");

            }
        }
    }
}



