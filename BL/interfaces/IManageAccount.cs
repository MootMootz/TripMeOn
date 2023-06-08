using System;
using System.Collections.Generic;
using TripMeOn.Models.Users;

namespace TripMeOn.BL.interfaces
{
    /// <summary>
    /// Cette interface permet de récuperer les utilisateurs de l'application pour que l'admin puisse les modifier
    /// </summary>
    public interface IManageAccount : IDisposable
    {
        List<Partner> GetAllPartners();
        List<Client> GetAllClients();
        List<Employee> GetAllEmployees();

        void DeletePartner(Partner partner);
        void DeleteClient(Client client);
        void DeleteEmployee(Employee employee);
    }
}
