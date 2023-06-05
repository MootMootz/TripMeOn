using TripMeOn.Models.Users;

namespace TripMeOn.ViewModels
{
    public class EmployeeViewModel
    {
        //pour login des employées
        public Employee Employee { get; set; }
        public bool AuthentifyE { get; set; }

        // pour ajouter un employée
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
