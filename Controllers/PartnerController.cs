using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class PartnerController : Controller

    {


        //private readonly IUserService _userService;

        //public PartnerController(IUserService userService);

        //public IActionResult IndexPartner()

        //{
        //    _userService = userService;
        //}

        private BddContext _bddContext;

        public PartnerController()
        {
            _bddContext = new BddContext();
        }

        public IActionResult ServicesPartner()
        {
            return View();
        }
        public IActionResult IndexPartner()
        {
            return View();
        }


        [HttpGet]
        public IActionResult PartnerForm()
        {
            var viewModel = new NavigationPartnerForm();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitPartnerForm(NavigationPartnerForm model)
        {

            using (var dbContext = new BddContext())
            {

                var partner = new Partner
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    CompanyName = model.CompanyName
                };

                dbContext.Partners.Add(partner);
                dbContext.SaveChanges();

                return RedirectToAction("SignUpConfirmation");
            }
        }


        public IActionResult SignUpConfirmation()
        {
            return View();
        }
        public IActionResult AddService()
        {
            return View();
        }

        public IActionResult PropositionService()
        {
            return View();
        }

        public IActionResult CreateRestaurant()
        {
            var partners = _bddContext.Partners.ToList(); // on récupere la liste des partenaires à partir de la base de donnée
            var destinations = _bddContext.Destinations.ToList(); // on récupere la liste des destinations à partir de la base de donnée
            ViewBag.PartnerList = partners.Select(d => new SelectListItem // on crée une liste SelecListItem pour créer dropdownlist.
                                                                          // Chaque SelecListItem a une Value donc ici c'est l'Id du partenaire et le Text sera le prenom,
                                                                          // le nom et le nom de la compagnie. ViewBag.PartnerList permet d'acceder depuis la vue CreateRestaurant
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}"
            }).ToList();
            ViewBag.DestinationList = destinations.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.Country}, {d.Region}, {d.City}"
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return View(restaurant);
            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateRestaurant(restaurant.Name, restaurant.Type, restaurant.Price, restaurant.PartnerId, restaurant.DestinationId, restaurant.StartDate, restaurant.EndDate);
                TempData["SuccessMessage"] = "Restaurant created successfully!";
                return RedirectToAction("ListeRestaurant");
            }
        }
        public IActionResult CreateAccomodation()
        {
            var partners = _bddContext.Partners.ToList();
            var destinations = _bddContext.Destinations.ToList();
            ViewBag.PartnerList = partners.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}"
            }).ToList();
            ViewBag.DestinationList = destinations.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.Country}, {d.Region}, {d.City}"
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateAccomodation(Accomodation accomodation)
        {
            if (!ModelState.IsValid)
                return View(accomodation);
            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateAccomodation(accomodation.Name, accomodation.Type, accomodation.Capacity, accomodation.Price, accomodation.PartnerId, accomodation.DestinationId, accomodation.StartDate, accomodation.EndDate);
                TempData["SuccessMessage"] = "Accomodation created successfully!";
                return RedirectToAction("ListeAccomodation");
            }
        }
        public IActionResult CreateTransportation()
        {
            var partners = _bddContext.Partners.ToList();
            var destinations = _bddContext.Destinations.ToList();
            ViewBag.PartnerList = partners.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}"
            }).ToList();
            ViewBag.DestinationList = destinations.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.Country}, {d.Region}, {d.City}"
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateTransportation(Transportation transportation)
        {
            if (!ModelState.IsValid)
                return View(transportation);
            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateTransportation(transportation.Type, transportation.Price, transportation.PartnerId, transportation.DestinationId, transportation.StartDate, transportation.EndDate);
                TempData["SuccessMessage"] = "Transportation created successfully!";
                return RedirectToAction("ListeTransportation");
            }
        }
        public IActionResult ModifyAccomodation(int id) // afficher la vue de modification de l'accomodation
        {
            if (id != 0)
            {
                using (IPropositionService propositionService = new PropositionService())
                {
                    Accomodation accomodation = propositionService.GetAllAccomodations().Where(r => r.Id == id).FirstOrDefault();
                    if (accomodation == null)
                    {
                        return View("Error");
                    }
                    var partners = _bddContext.Partners.ToList();
                    var destinations = _bddContext.Destinations.ToList();

                    ViewBag.PartnerList = partners.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                        Selected = d.Id == accomodation.PartnerId  // Ajoutez cette ligne
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == accomodation.DestinationId  // Ajoutez cette ligne
                    }).ToList();
                    return View(accomodation);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifyAccomodation(Accomodation accomodation) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(accomodation);
            if (accomodation.Id != 0)
            {
                using (PropositionService propositionService = new PropositionService())
                {
                    propositionService.ModifyAccomodation(accomodation);
                    TempData["SuccessMessage"] = "The accomodation has been modified";
                    return RedirectToAction("ListeAccomodation");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult ModifyRestaurant(int id) // afficher la vue de modification du restaurant
        {
            if (id != 0)
            {
                using (IPropositionService propositionService = new PropositionService())
                {
                    Restaurant restaurant = propositionService.GetAllRestaurants().Where(r => r.Id == id).FirstOrDefault();
                    if (restaurant == null)
                    {
                        return View("Error");
                    }
                    var partners = _bddContext.Partners.ToList();
                    var destinations = _bddContext.Destinations.ToList();

                    ViewBag.PartnerList = partners.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                        Selected = d.Id == restaurant.PartnerId  // Ajoutez cette ligne
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == restaurant.DestinationId  // Ajoutez cette ligne
                    }).ToList();

                    return View(restaurant);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifyRestaurant(Restaurant restaurant) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(restaurant);
            if (restaurant.Id != 0)
            {
                using (PropositionService propositionService = new PropositionService())
                {
                    propositionService.ModifyRestaurant(restaurant);
                    TempData["SuccessMessage"] = "The restaurant has been modified";
                    return RedirectToAction("ListeRestaurant");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult ModifyTransportation(int id) // afficher la vue de modification de transportation
        {
            if (id != 0)
            {
                using (IPropositionService propositionService = new PropositionService())
                {
                    Transportation transportation = propositionService.GetAllTransportations().Where(r => r.Id == id).FirstOrDefault();
                    if (transportation == null)
                    {
                        return View("Error");
                    }
                    var partners = _bddContext.Partners.ToList();
                    var destinations = _bddContext.Destinations.ToList();

                    ViewBag.PartnerList = partners.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                        Selected = d.Id == transportation.PartnerId  // Ajoutez cette ligne
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == transportation.DestinationId  // Ajoutez cette ligne
                    }).ToList();
                    return View(transportation);
                }
            }
            return View("Error");
        }
        [HttpPost]
        public IActionResult ModifyTransportation(Transportation transportation) // traiter la requête de modification
        {
            if (!ModelState.IsValid)
                return View(transportation);
            if (transportation.Id != 0)
            {
                using (PropositionService propositionService = new PropositionService())
                {
                    propositionService.ModifyTransportation(transportation);
                    TempData["SuccessMessage"] = "The transportation has been modified";
                    return RedirectToAction("ListeTransportation");
                }
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult ToggleOnlineStatusAccomodation(int id)
        {
            using (PropositionService propositionService = new PropositionService())
            {
                Accomodation accomodation = propositionService.GetAllAccomodations().Where(r => r.Id == id).FirstOrDefault();
                if (accomodation != null)
                {
                    accomodation.IsOnline = !accomodation.IsOnline;  // inverse l'état actuel
                    propositionService.ModifyAccomodation(accomodation); // sauvegarde les modifications
                    TempData["SuccessMessage"] = $"The accomodation was put {(accomodation.IsOnline ? "online" : "offline")}.";
                }
            }
            return RedirectToAction("ListeAccomodation"); // redirige vers la liste des accomodations
        }
        public IActionResult ToggleOnlineStatusRestaurant(int id)
        {
            using (PropositionService propositionService = new PropositionService())
            {
                Restaurant restaurant = propositionService.GetAllRestaurants().Where(r => r.Id == id).FirstOrDefault();
                if (restaurant != null)
                {
                    restaurant.IsOnline = !restaurant.IsOnline;  // inverse l'état actuel
                    propositionService.ModifyRestaurant(restaurant); // sauvegarde les modifications
                    TempData["SuccessMessage"] = $"The restaurant was put {(restaurant.IsOnline ? "online" : "offline")}.";
                }
            }
            return RedirectToAction("ListeRestaurant"); // redirige vers la liste des restaurants
        }
        public IActionResult ToggleOnlineStatusTransportation(int id)
        {
            using (PropositionService propositionService = new PropositionService())
            {
                Transportation transportation = propositionService.GetAllTransportations().Where(r => r.Id == id).FirstOrDefault();
                if (transportation != null)
                {
                    transportation.IsOnline = !transportation.IsOnline;  // inverse l'état actuel
                    propositionService.ModifyTransportation(transportation); // sauvegarde les modifications
                    TempData["SuccessMessage"] = $"The transportation was put {(transportation.IsOnline ? "online" : "offline")}.";
                }
            }
            return RedirectToAction("ListeTransportation"); // redirige vers la liste des transportations
        }
        public IActionResult ListeAccomodation()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                viewModel.Accomodations = propositionService.GetAllAccomodations();
                return View(viewModel);
            }
        }
        public IActionResult ListeRestaurant()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                viewModel.Restaurants = propositionService.GetAllRestaurants();
                return View(viewModel);
            }
        }
        public IActionResult ListeTransportation()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                viewModel.Transportations = propositionService.GetAllTransportations();
                return View(viewModel);
            }
        }

        public IActionResult SelectService(string service)
        {
            switch (service)
            {
                case "Accomodation":
                    return RedirectToAction("CreateAccomodation");
                case "Restaurant":
                    return RedirectToAction("CreateRestaurant");
                case "Transportation":
                    return RedirectToAction("CreateTransportation");
                default:
                    return View();
            }
        }

        //public IActionResult DeleteRestaurant(int id) // afficher la vue de modification du restaurant
        //{
        //    if (id != 0)
        //    {
        //        using (IPropositionService propositionService = new PropositionService())
        //        {
        //            Restaurant restaurant = propositionService.GetAllRestaurants().Where(r => r.Id == id).FirstOrDefault();
        //            if (restaurant == null)
        //            {
        //                return View("Error");
        //            }
        //            return View(restaurant);
        //        }
        //    }
        //    return View("Error");
        //}
        //[HttpPost]
        //public IActionResult DeleteRestaurant(Restaurant restaurant) // traiter la requête de modification
        //{
        //    if (!ModelState.IsValid)
        //        return View(restaurant);
        //    if (restaurant.Id != 0)
        //    {
        //        using (PropositionService propositionService = new PropositionService())
        //        {
        //            propositionService.DeleteRestaurant(restaurant);
        //            return RedirectToAction("ListeRestaurant", new { @id = restaurant.Id });
        //        }
        //    }
        //    else
        //    {
        //        return View("Error");
        //    }
        //}
    }
}
