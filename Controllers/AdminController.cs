using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TripMeOn.Models.Admin;

namespace TripMeOn.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Package()
        {
            return View();
        }

        public IActionResult Customer()
        {

            List<Customer> customer = new List<Customer>()
            {
                new Customer {Id = 1, Name="Name1", Address="Address1" },
                new Customer {Id = 2, Name="Name2", Address="Address2" },
                new Customer {Id = 3, Name="Name3", Address="Address3" }
            };

            return View(customer);
        }

        public IActionResult Partner()
        {
            return View();
        }
    }
}
