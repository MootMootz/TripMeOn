using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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



        public IActionResult PartnerForm()
        {
            var viewModel = new ClientViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Méthode que récupère le client qui vient de s'authentifier 
        /// </summary>
        /// <returns></returns>
        private NavigationViewModel SetupNavigationViewModel()
        {
            var viewModel = new NavigationViewModel
            {
                AuthentifyC = HttpContext.User.Identity.IsAuthenticated
            };

            if (viewModel.AuthentifyC)
            {
                viewModel.Client = userService.GetClient(HttpContext.User.Identity.Name);
            }

            return viewModel;
        }

        public IActionResult CheckoutForm()
        {
            return View();
        }
        //LOG IN CLIENT
        /// <summary>
        /// Méthode pour aller sur le login client
        /// </summary>
        /// <returns>page pour faire le login</returns>
        public IActionResult LoginClient()
        {
            ViewBag.ShowLoginClientBox = true;
            return View(SetupNavigationViewModel());
        }
        /// <summary>
        /// Méthode get d'authentification. On crée une instance de NavigationViewModel
        /// </summary>

        public IActionResult IndexClient(string returnUrl)
        {
            var viewModel = new NavigationViewModel(); // Create an instance of the 
            if (!viewModel.AuthentifyC)
            {
                ViewBag.ShowLoginClientBox = true;
            }

            return View("LoginClient", viewModel); // Pass the NavigationViewModel to the LoginClient view
        }
        /// <summary>
        /// Méthode post de l'information pour l'authentification du client
        /// </summary>
        /// <param name="viewModel">si l'authentification ne se réalise pas, on revient sur la page principal</param>
        /// <param name="returnUrl">si l'authentification marche, on va sur la page où on se dirigé avant que l'application demande l'authentification</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult IndexClient(NavigationViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Client client = userService.AuthentifyC(viewModel.Client.Nickname, viewModel.Client.Password);
                if (client != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, client.Id.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);
                    var cookieOptions = new CookieOptions // on crée un nouvel objet CookieOptions
                    {
                        HttpOnly = true, // cette cookie sera accessible que par le serveur, pas par le coté client
                        Expires = DateTime.UtcNow.AddDays(30), // on dit que 30 jours apres le cookie va expirer. Il peut etre utilie pour se souvenir de moi etc...
                    };
                    Response.Cookies.Append("Nickname", client.Nickname, cookieOptions); // ajout le cookier au Response et envoit au navigateur


                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("ViewCart", "Cart"); 
                }
                ModelState.AddModelError("Client.Nickname", "Incorrect Nickname or Password");
            }
            ViewBag.ShowLoginClientBox = true;
            return View(viewModel);
        }

        //LOG IN PARTENAIRE
        public IActionResult LoginPartner()
        {
            ViewBag.ShowLoginPartnerBox = true;
            return View(SetupNavigationViewModel());
        }
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
                    var cookieOptions = new CookieOptions // on crée un nouvel objet CookieOptions
                    {
                        HttpOnly = true, // cette cookie sera accessible que par le serveur, pas par le coté client
                        Expires = DateTime.UtcNow.AddDays(30), // on dit que 30 jours apres le cookie va expirer. Il peut etre utilie pour se souvenir de moi etc...
                    };
                    Response.Cookies.Append("Nickname", partner.Nickname, cookieOptions); // ajout le cookier au Response et envoit au navigateur


                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("IndexPartner");
                }
                ModelState.AddModelError("Partner.Nickname", "Incorrect username or password");
            }
            return View("../Home/HomePage", viewModel);
        }
        /// <summary>
        /// Méthodde de déconnection. Les cookies sont effacés du système
        /// </summary>
        /// <returns>on revient sur la page principal</returns>
        public ActionResult Deconnection()
        {
            HttpContext.SignOutAsync();
            Response.Cookies.Delete("Nickname");
            return Redirect("../Home/HomePage");
        }

        //LOG IN EMPLOYE
        public IActionResult LoginAdmin()
        {
            ViewBag.ShowLoginAdminBox = true;
            return View(SetupNavigationViewModel());
        }
        public IActionResult IndexAdmin()
        {
            NavigationViewModel viewModel = new NavigationViewModel { AuthentifyE = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.AuthentifyE)
            {
                viewModel.Employee = userService.GetEmployee(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult IndexAdmin(NavigationViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Employee employee = userService.AuthentifyE(viewModel.Employee.Nickname, viewModel.Employee.Password);
                if (employee != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(userPrincipal);
                    var cookieOptions = new CookieOptions // on crée un nouvel objet CookieOptions
                    {
                        HttpOnly = true, // cette cookie sera accessible que par le serveur, pas par le coté client
                        Expires = DateTime.UtcNow.AddDays(30), // on dit que 30 jours apres le cookie va expirer. Il peut etre utilie pour se souvenir de moi etc...
                    };
                    Response.Cookies.Append("Nickname", employee.Nickname, cookieOptions); // ajout le cookier au Response et envoit au navigateur


                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("../Employee/Notifications");
                }
                ModelState.AddModelError("Employee.Nickname", "le nom ou le mot de passe sont incorrects");
            }
            return View("../Home/HomePage", viewModel);
        }

       
    }
}


