using System;
using System.Collections.Generic;
using TripMeOn.Models.Users;

namespace TripMeOn.BL.interfaces
{
    public interface IUserService:IDisposable
    {
		void DeleteCreateDatabase();
		
		//à faire juste après -qqs
		//int AddPartner(string nom, string password);
		Partner AuthentifyP(string nickname, string password);
		Partner GetPartner(int id);
		Partner GetPartner(string idStr);

		//authentification client
		Client AuthentifyC(string nickname, string password);	
		Client GetClient(int id);	
		Client GetClient(string idSr);

	}
}

