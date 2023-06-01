using TripMeOn.Models.Users;

namespace TripMeOn.ViewModels
{
    public class ClientViewModel
    {
        //pour le login du client
        public Client Client{ get; set; }
        public bool AuthentifyC { get; set; }

        //pour ajouter un nouveau client
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientType { get; set; }
    }
}
