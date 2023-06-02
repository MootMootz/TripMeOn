using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using TripMeOn.Models.Users;
using System.Security.Cryptography;

namespace TripMeOn.BL
{
    public class UserService//:IUserService
    {
        private BddContext _bddContext;
        public UserService()
        {
            _bddContext = new BddContext();
        }

        // CLIENT _ AUTHENTIFICATION
        public Client AuthentifyC(string nickname, string password)
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



        // PARTNER _ AUTHENTIFICATION

        public Partner AuthentifyP(string nickname, string password)
        {
            string motDePasse = EncodeMD5(password);
            Partner partner = this._bddContext.Partners.FirstOrDefault(u => u.Nickname == nickname && u.Password == motDePasse);
            return partner;
        }

        public Partner GetPartner(int id)
        {
            return this._bddContext.Partners.Find(id);
        }

        public Partner GetPartner(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetPartner(id);
            }
            return null;
        }

        // EMPLOYEE _ AUTHENTIFICATION
        public Employee AuthentifyE(string nickname, string password)
        {
            string motDePasse = EncodeMD5(password);
            Employee employee = this._bddContext.Employees.FirstOrDefault(u => u.Nickname == nickname && u.Password == motDePasse);
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return this._bddContext.Employees.Find(id);
        }

        public Employee GetEmployee(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetEmployee(id);
            }
            return null;
        }

        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "UnUser" + motDePasse + "ASP.NET MVC";
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
