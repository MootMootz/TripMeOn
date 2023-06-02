﻿using Microsoft.AspNetCore.Authentication;
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


        //LOG IN CLIENT
        public IActionResult LoginClient()
        {
            return View();
        }
        public IActionResult IndexClient()
        {
            NavigationViewModel viewModel = new NavigationViewModel { AuthentifyC = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.AuthentifyC)
            {
                viewModel.Client = userService.GetClient(HttpContext.User.Identity.Name);
                return View(viewModel);
            }
            return View(viewModel);
        }

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

                    return Redirect("../Partner/IndexPartner");
                }
                ModelState.AddModelError("Client.Nickname", "le nom ou le mot de passe sont incorrects");
            }
            return View("../Home/HomePage", viewModel);
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
                    var cookieOptions = new CookieOptions // on crée un nouvel objet CookieOptions
                    {
                        HttpOnly = true, // cette cookie sera accessible que par le serveur, pas par le coté client
                        Expires = DateTime.UtcNow.AddDays(30), // on dit que 30 jours apres le cookie va expirer. Il peut etre utilie pour se souvenir de moi etc...
                    };
                    Response.Cookies.Append("Nickname", partner.Nickname, cookieOptions); // ajout le cookier au Response et envoit au navigateur


                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return Redirect("../Partner/IndexPartner");
                }
                ModelState.AddModelError("Partner.Nickname", "le nom ou le mot de passe sont incorrects");
            }
            return View("../Home/HomePage", viewModel);
        }

        //LOG IN EMPLOYE
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
                Employee employee= userService.AuthentifyE(viewModel.Employee.Nickname, viewModel.Employee.Password);
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

                    return Redirect("../Employee/IndexAdmin");
                }
                ModelState.AddModelError("Employee.Nickname", "le nom ou le mot de passe sont incorrects");
            }
            return View("../Home/HomePage", viewModel);
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("HomePage", "Home");

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


