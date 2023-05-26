using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TripMeOn.BL;
using TripMeOn.Models.Users;
using TripMeOn.ViewModels;

namespace TripMeOn.Controllers
{
    public class LoginController : Controller
    {
        private UserService userService;
        public LoginController()
        {
            userService = new UserService();
        }

        //il va falloir changer les returns
        public IActionResult Index()
        {
            ClientViewModel viewModel = new ClientViewModel { Authentify = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentify)
            {
                viewModel.client = userService.GetClient(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(ClientViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Client client = userService.Authentify(viewModel.client.Nickname, viewModel.client.Password);
                if (client != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, client.Id.ToString()),
                       //new Claim(ClaimTypes.Role, utilisateur.Role),

                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("/");
                }
                ModelState.AddModelError("Utilisateur.Prenom", "Prénom et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        //A FAIRE JUSTE APRES

        //public IActionResult CreerCompte()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult CreerCompte(Utilisateur utilisateur)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int id = dal.AjouterUtilisateur(utilisateur.Prenom, utilisateur.Password);

        //        var userClaims = new List<Claim>()
        //        {
        //            new Claim(ClaimTypes.Name, id.ToString()),
        //        };

        //        var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

        //        var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
        //        HttpContext.SignInAsync(userPrincipal);

        //        return Redirect("/");
        //    }
        //    return View(utilisateur);
        //}

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}

