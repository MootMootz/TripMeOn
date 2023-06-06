using TripMeOn.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripMeOn.ViewModels
{
    public class NavigationViewModel
    {
        public string Logo { get; set; }
        public string AboutUs { get; set; }
        public string Destination { get; set; }
        public string HolidayFinder { get; set; }
        public string Theme { get; set; }
        public string Connect { get; set; }

        //pour la méthode d'authentification du partenaire -qqs
        public Partner Partner { get; set; }
        public bool AuthentifyP { get; set; }

        //pour la méthode d'authentification du partenaire -qqs
        public Client Client { get; set; }
        public bool AuthentifyC { get; set; }

        //pour la méthode d'authentification des employes -qqs
        public Employee Employee { get; set; }
        public bool AuthentifyE { get; set; }
    }
}
