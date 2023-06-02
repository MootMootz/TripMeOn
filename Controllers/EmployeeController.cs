using Microsoft.AspNetCore.Mvc;
using TripMeOn.BL;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }

        //méthode pour ajouter un employée

        private Models.BddContext _bddContext;

        public EmployeeController()
        {
            _bddContext = new Models.BddContext();
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
                    JobTitle = model.JobTitle
                };

                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                return View("../Employee/IndexAdmin");
            }
        }

    }
}
