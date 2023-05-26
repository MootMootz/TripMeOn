using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;
using XSystem.Security.Cryptography;

namespace TripMeOn.BL
{
    public class UserService : IUserService
    {
        private BddContext _bddContext;
        public UserService()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        //public int AjouterUtilisateur(string prenom, string password)
        //{
        //    string motDePasse = EncodeMD5(password);
        //    Utilisateur user = new Utilisateur() { Prenom = prenom, Password = motDePasse };
        //    this._bddContext.Utilisateurs.Add(user);
        //    this._bddContext.SaveChanges();
        //    return user.Id;
        //}

        // AUTHENTIFICATION CLIENT
        public Client Authentify(string nickname, string password)
        {
            string motDePasse = EncodeMD5(password);
            Client client= this._bddContext.Clients.FirstOrDefault(u => u.Nickname == nickname && u.Password == motDePasse);
            return client;
        }

        public Client GetClient(int id)
        {
            return this._bddContext.Clients.Find(id);
        }

        public Client GetClient(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetClient(id);
            }
            return null;
        }

        public static string EncodeMD5(string motDePasse)
        {
            //je ne sais pas ce qui fait ce "Choix Resto" là -qqs
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

    }





}
    
