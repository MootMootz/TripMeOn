using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Users;

namespace TripMeOn.BL
{
    public class ManageAccount : IManageAccount
    {
        private BddContext _bddContext;
        /// <summary>
        /// On instancie la basse de données
        /// </summary>
        public ManageAccount()
        {
            _bddContext = new BddContext();
        }
        /// <summary>
        /// On récupère tout les utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<Partner> GetAllPartners()
        {
            return _bddContext.Partners.ToList();
        }

        public List<Client> GetAllClients()
        {
            return _bddContext.Clients.ToList();
        }
        public List<Employee> GetAllEmployees()
        {
            return _bddContext.Employees.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
        /// <summary>
        /// Les méthodes "Modify" appliquent les modifications sur l'objet choisit et enregistre ces modifications dans la base de données
        /// </summary>
        /// <param name="partner">la méthode récupère le partenaire (client ou employé) choisi pour réaliser les modifications</param>
        public void ModifyPartner(Partner partner) 
        {
            _bddContext.Partners.Update(partner);
            _bddContext.SaveChanges();
        }
        public void ModifyClient(Client client) 
        {
            _bddContext.Clients.Update(client);
            _bddContext.SaveChanges();
        }
        public void ModifyEmployee(Employee employee)
        { 
            _bddContext.Employees.Update(employee);
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Les méthodes Delete appliquent les  modifications sur l'objet et enregistre ces modifications dans la base de données
        /// </summary>
        /// <param name="partner"></param>
        public void DeletePartner(Partner partner) 
        {
            _bddContext.Partners.Remove(partner);
            _bddContext.SaveChanges();
        }
        public void DeleteClient(Client client) 
        {
            _bddContext.Clients.Remove(client);
            _bddContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee) 
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();
        }


    }
}
