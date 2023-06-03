using System;
using System.Collections.Generic;
using TripMeOn.Models.Users;

namespace TripMeOn.BL.interfaces
{
    public interface IManageAccount : IDisposable
    {
        List<Partner> GetAllPartners();
        List<Client> GetAllClients();

        void DeletePartner(Partner partner);
        void DeleteClient(Client client);
    }
}
