using System;
using System.Collections.Generic;
using TripMeOn.Models.Users;

namespace TripMeOn.BL.interfaces
{
	/// <summary>
	/// Interface utilisé principalement pour la méthode d'authentification des utilisateurs
	/// </summary>
    public interface IUserService:IDisposable
    {
	
		
		//int AddPartner(string nom, string password);
		Partner AuthentifyP(string nickname, string password);
		Partner GetPartner(int id);
		Partner GetPartner(string idStr);

		//authentification client
		
		Client AuthentifyC(string nickname, string password);	
		Client GetClient(int id);	
		Client GetClient(string idSr);

		//authentification employee
		Employee AuthentifyE(string nickname, string password);
		Employee GetEmployee(int id);
		Employee GetEmployee(string idStr);


	}
}

