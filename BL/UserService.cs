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
    public class UserService//:IUserService
    {
        private BddContext _bddContext;
        public UserService()
        {
            _bddContext = new BddContext();
        }
        public Client Authentify(string nickname, string password)
        {
            string motDePasse = EncodeMD5(password);
            Client client = this._bddContext.Clients.FirstOrDefault(u => u.Nickname == nickname && u.Password == motDePasse);
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

        //j'ignore à quoi sert "unCLient" mais je le laisse -qqs, 
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "UnClient" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }



        // Create

        // Modify

        // Delete

        // etc...
    }
    }
