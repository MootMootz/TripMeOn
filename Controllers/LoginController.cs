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
        private readonly UserService userService;
        public LoginController()
        {
            userService = new UserService();
        }




        //LOG IN CLIENT
        //      il va falloir changer les returns
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
                ModelState.AddModelError("Client.Nickname", "le nom pu le mot de passe sont incorrects");
            }
            return View(viewModel);
        }

        //LOG IN PARTENAIRE
        public IActionResult IndexPartner()
        {
            NavigationViewModel viewModel = new NavigationViewModel { AuthentifyP = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.AuthentifyP)
            {
                viewModel.Partner = userService.GetPartner(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult IndexPartner(NavigationViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Partner partner = userService.AuthentifyP(viewModel.Partner.Nickname, viewModel.Partner.Password);
                if (partner != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, partner.Id.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("../Partner/IndexPartner");
                }
                ModelState.AddModelError("Partner.Nickname", "le nom ou le mot de passe sont incorrects");
            }
            return View("../Home/HomePage",viewModel);
        }

        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction ("IndexPartner");
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

        //        public ActionResult Deconnexion()
        //        {
        //            HttpContext.SignOutAsync();
        //            return Redirect("/");
        //        }
    }
            }
        
    
