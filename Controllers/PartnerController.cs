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

        public IActionResult ModifyAccomodation(int id)
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
    }
}
