﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Admin;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class PartnerController : Controller

    {

        private Models.BddContext _bddContext;
        private IPropositionService _propositionService;

        public PartnerController(IPropositionService propositionService)
        {
            _bddContext = new Models.BddContext();
            _propositionService = propositionService;
        }

        public IActionResult ServicesPartner()
        {
            return View();
        }
        public IActionResult IndexPartner()
        {
            return View();
        }

        /// <summary>
        /// Les deux méthodes suivantes servent à la création d'un nouveau partenaire.
        /// </summary>
        /// <returns>L'information est envoyé sur la bdd</returns>
        [HttpGet]
        public IActionResult PartnerForm()
        {
            var viewModel = new NavigationPartnerForm();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitPartnerForm(NavigationPartnerForm model)
        {

            using (var dbContext = new Models.BddContext())
            {

                var partner = new Models.Users.Partner
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Nickname = model.Nickname,
                    Email = model.Email,
                    Password = UserService.EncodeMD5(model.Password),
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    CompanyName = model.CompanyName
                };

                dbContext.Partners.Add(partner);
                dbContext.SaveChanges();

                return base.RedirectToAction("SignUpConfirmation");
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
        /// <summary>
        /// Les méthoddes suivantes servent à créer des services partenaires à partir de la plateforme partenaire en remplissant un formulaire
        /// </summary>
        /// <returns>les donnés iront sur la bdd et une notification sera reçu à l'admin pour traiter l'information</returns>
        public IActionResult CreateRestaurant()
        {
            string partnerId = User.FindFirstValue(ClaimTypes.Name);
            var partners = _bddContext.Partners.ToList(); // on récupere la liste des partenaires à partir de la base de donnée
            var destinations = _bddContext.Destinations.ToList(); // on récupere la liste des destinations à partir de la base de donnée
            ViewBag.PartnerList = partners.Select(d => new SelectListItem // on crée une liste SelecListItem pour créer dropdownlist.
                                                                          // Chaque SelecListItem a une Value donc ici c'est l'Id du partenaire et le Text sera le prenom,
                                                                          // le nom et le nom de la compagnie. ViewBag.PartnerList permet d'acceder depuis la vue CreateRestaurant
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                Selected = d.Id.ToString() == partnerId
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
                int id = propositionService.CreateRestaurant(restaurant.Name, restaurant.Type, restaurant.Price, restaurant.PartnerId, restaurant.DestinationId, restaurant.StartDate, restaurant.EndDate, restaurant.Description);
                TempData["SuccessMessage"] = "Restaurant created successfully!";
                return RedirectToAction("ListeRestaurant");
            }
        }
        public IActionResult CreateAccomodation()
        {
            string partnerId = User.FindFirstValue(ClaimTypes.Name); // permet d'obtenir la valeur de claim pour l'utilisateur authentifié
            var partners = _bddContext.Partners.ToList();
            var destinations = _bddContext.Destinations.ToList();
            ViewBag.PartnerList = partners.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                Selected = d.Id.ToString() == partnerId // permet de preselectionner partenaire si l'ID du partenaire correspond à partnerId
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
                int id = propositionService.CreateAccomodation(accomodation.Name, accomodation.Type, accomodation.Capacity, accomodation.Price, accomodation.PartnerId, accomodation.DestinationId, accomodation.StartDate, accomodation.EndDate, accomodation.Description);
                TempData["SuccessMessage"] = "Accomodation created successfully!";
                return RedirectToAction("ListeAccomodation");
            }
        }
        public IActionResult CreateTransportation()
        {
            string partnerId = User.FindFirstValue(ClaimTypes.Name);
            var partners = _bddContext.Partners.ToList();
            var destinations = _bddContext.Destinations.ToList();
            ViewBag.PartnerList = partners.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = $"{d.FirstName} {d.LastName}, {d.CompanyName}",
                Selected = d.Id.ToString() == partnerId
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
                int id = propositionService.CreateTransportation(transportation.Type, transportation.Price, transportation.PartnerId, transportation.DestinationId, transportation.StartDate, transportation.EndDate, transportation.Description);
                TempData["SuccessMessage"] = "Transportation created successfully!";
                return RedirectToAction("ListeTransportation");
            }
        }
        /// <summary>
        /// Méthodes pour modifier les services existantes
        /// </summary>
        /// <param name="id">le service est choisi et géré par son id</param>
        /// <returns>le service avec les modifications</returns>
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
                        Selected = d.Id == accomodation.PartnerId
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == accomodation.DestinationId
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
                        Selected = d.Id == restaurant.PartnerId
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == restaurant.DestinationId
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
                        Selected = d.Id == transportation.PartnerId
                    }).ToList();

                    ViewBag.DestinationList = destinations.Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = $"{d.Country}, {d.Region}, {d.City}",
                        Selected = d.Id == transportation.DestinationId
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
        /// <summary>
        /// Méthodes pour mettre en ligne les services des partenaires
        /// </summary>
        /// <param name="id">le service est cheché dans la bdd par son id</param>
        /// <returns>le service sera en ligne, ou pas, selon l'option choisit</returns>
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
        /// <summary>
        /// Méthode pour afficher la vue de tous les services, selon le choix fait ( hébergement, restaurants, ou transports)
        /// </summary>
        /// <returns></returns>
        public IActionResult ListeAccomodation()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Accomodations = propositionService.GetAccomodationsByPartnerId(partnerId);
                return View(viewModel);
            }
        }
        public IActionResult ListeRestaurant()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Restaurants = propositionService.GetRestaurantsByPartnerId(partnerId);
                return View(viewModel);
            }
        }
        public IActionResult ListeTransportation()
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                PropositionServiceModel viewModel = new PropositionServiceModel();
                int partnerId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
                viewModel.Transportations = propositionService.GetTransportationsByPartnerId(partnerId);
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

        public IActionResult DeleteRestaurant(int id)
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                Restaurant restaurant = propositionService.GetAllRestaurants().Where(r => r.Id == id).FirstOrDefault();
                propositionService.DeleteRestaurant(restaurant);
                return RedirectToAction("ListeRestaurant");
            }
        }
        public IActionResult DeleteAccomodation(int id)
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                Accomodation accomodation = propositionService.GetAllAccomodations().Where(r => r.Id == id).FirstOrDefault();
                propositionService.DeleteAccomodation(accomodation);
                return RedirectToAction("ListeAccomodation");
            }
        }
        public IActionResult DeleteTransportation(int id)
        {
            using (IPropositionService propositionService = new PropositionService())
            {
                Transportation transportation = propositionService.GetAllTransportations().Where(r => r.Id == id).FirstOrDefault();
                propositionService.DeleteTransportation(transportation);
                return RedirectToAction("ListeTransportation");
            }
        }

        /// <summary>
        /// Méthode de recerche multicritère d'un service
        /// </summary>
        /// <param name="serviceType">recherche par type de service</param>
        /// <param name="destination">recherche par destination</param>
        /// <param name="month">recherche par mois</param>
        /// <returns>les paquets qui correspondent aux critères choisis</returns>
        public IActionResult SearchPackage(string serviceType, int destination)
        {
            var searchResults = _propositionService.SearchByServiceTypeDestination(
                serviceType,
                destination == 0 ? (int?)null : destination);
            var viewModel = new PropositionServiceModel();

            switch (serviceType)
            {
                case "Accomodation":
                    viewModel.Accomodations = searchResults.Cast<Accomodation>().ToList();
                    break;
                case "Restaurant":
                    viewModel.Restaurants = searchResults.Cast<Restaurant>().ToList();
                    break;
                case "Transport":
                    viewModel.Transportations = searchResults.Cast<Transportation>().ToList();
                    break;
                default:
                    break;
            }

            return View("SearchServicePackages", viewModel);
        }
        /// <summary>
        /// méthodes d'affichage de service choisi et ses détails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DetailsAccomodations(int id)
        {
            var accomodation = _propositionService.GetAllAccomodations().FirstOrDefault(a => a.Id == id);

            if (accomodation == null)
            {
                return NotFound();
            }


            return View(accomodation);
        }

        public IActionResult DetailsRestaurants(int id)
        {
            var restaurant = _propositionService.GetAllRestaurants().FirstOrDefault(a => a.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }


            return View(restaurant);
        }

        public IActionResult DetailsTransportations(int id)
        {
            var transportation = _propositionService.GetAllTransportations().FirstOrDefault(a => a.Id == id);

            if (transportation == null)
            {
                return NotFound();
            }


            return View(transportation);
        }



    }
}
