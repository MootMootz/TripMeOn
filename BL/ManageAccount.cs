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

        public ManageAccount()
        {
            _bddContext = new BddContext();
        }

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

        public void ModifyPartner(Partner partner) // applique les modifications sur accomodation et enregistre ces modifications dans la base de données
        {
            _bddContext.Partners.Update(partner);
            _bddContext.SaveChanges();
        }
        public void ModifyClient(Client client) // applique les modifications sur accomodation et enregistre ces modifications dans la base de données
        {
            _bddContext.Clients.Update(client);
            _bddContext.SaveChanges();
        }
        public void ModifyEmployee(Employee employee) // applique les modifications sur accomodation et enregistre ces modifications dans la base de données
        {
            _bddContext.Employees.Update(employee);
            _bddContext.SaveChanges();
        }

        public void DeletePartner(Partner partner) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Partners.Remove(partner);
            _bddContext.SaveChanges();
        }
        public void DeleteClient(Client client) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Clients.Remove(client);
            _bddContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee) // applique les modifications sur restaurant et enregistre ces modifications dans la base de données
        {
            _bddContext.Employees.Remove(employee);
            _bddContext.SaveChanges();
        }


    }
}
