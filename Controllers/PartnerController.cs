using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
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
                    Name = model.Name,
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

        public IActionResult PropositionService()
        {
            var viewModel = new PropositionServiceModel();
            return View(viewModel);
        }

        public IActionResult CreateRestaurant()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRestaurant(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return View(restaurant);

            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateRestaurant(restaurant.Name, restaurant.Type, restaurant.Price, restaurant.PartnerId, restaurant.DestinationId);
                TempData["SuccessMessage"] = "Restaurant created successfully!";
                return RedirectToAction("ListeRestaurant", new { @id = id });
            }
        }

        public IActionResult CreateAccomodation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccomodation(Accomodation accomodation)
        {
            if (!ModelState.IsValid)
                return View(accomodation);

            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateAccomodation(accomodation.Name, accomodation.Type, accomodation.Capacity, accomodation.Price, accomodation.PartnerId, accomodation.DestinationId);
                TempData["SuccessMessage"] = "Accomodation created successfully!";
                return RedirectToAction("ListeAccomodation", new { @id = id });
            }
        }

        public IActionResult CreateTransportation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTransportation(Transportation transportation)
        {
            if (!ModelState.IsValid)
                return View(transportation);

            using (PropositionService propositionService = new PropositionService())
            {
                int id = propositionService.CreateTransportation(transportation.Type, transportation.Price, transportation.PartnerId, transportation.DestinationId);
                TempData["SuccessMessage"] = "Transportation created successfully!";
                return RedirectToAction("ListeTransportation", new { @id = id });
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
                    return RedirectToAction("ListeAccomodation", new { @id = accomodation.Id });
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
                    return RedirectToAction("ListeRestaurant", new { @id = restaurant.Id });
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
                    return RedirectToAction("ListeTransportation", new { @id = transportation.Id });
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
