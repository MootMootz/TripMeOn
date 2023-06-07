using System.Collections.Generic;
using TripMeOn.Models.Users;

namespace TripMeOn.ViewModels
{
    public class ManageAccountModel
    {
        public List<Partner> Partners { get; set; }
        public List<Client> Clients { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
