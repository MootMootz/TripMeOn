using Microsoft.AspNetCore.Mvc;

using TripMeOn.BL.interfaces;

using TripMeOn.BL;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;
using System.Linq;
using TripMeOn.Models.PartnerProducts;

namespace TripMeOn.Controllers
{
    /// <summary>
    /// Cette classe est utilisé pour les fonctions de l'interface de l'administrateur du site. On regarde les services, les partenaires, les clients, et on peut les modifier.
    /// </summary>
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

        public IActionResult ListeEmployee()
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                ManageAccountModel viewModel = new ManageAccountModel();
                viewModel.Employees = manageAccount.GetAllEmployees();
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

        public IActionResult ModifyEmployee(int id) // afficher la vue de modification de l'accomodation
        {
            if (id != 0)
            {
                using (IManageAccount manageAccount = new ManageAccount())
                {
                    Employee employee = manageAccount.GetAllEmployees().Where(r => r.Id == id).FirstOrDefault();
                    if (employee == null)
                    {
                        return View("Error");
                    }
                    return View(employee);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifyEmployee(Employee employee) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(employee);
            if (employee.Id != 0)
            {
                using (ManageAccount manageAccount = new ManageAccount())
                {
                    manageAccount.ModifyEmployee(employee);
                    TempData["SuccessMessage"] = "The employee has been modified";
                    return RedirectToAction("ListeEmployee");
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

        public IActionResult DeleteEmployee(int id)
        {
            using (IManageAccount manageAccount = new ManageAccount())
            {
                Employee employee = manageAccount.GetAllEmployees().Where(r => r.Id == id).FirstOrDefault();
                manageAccount.DeleteEmployee(employee);
                return RedirectToAction("ListeEmployee");
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

                return RedirectToAction("ListeEmployee");

            }
        }
    }
}



