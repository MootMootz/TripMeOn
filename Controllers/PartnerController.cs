using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TripMeOn.Models;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class PartnerController : Controller

    {
        public IActionResult IndexPartner()
        {
            return View();
        }

        public IActionResult ServicesPartner()
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
    }
}
