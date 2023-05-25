using System;
using System.Collections.Generic;

namespace TripMeOn.BL.interfaces
{
    public interface IProductService:IDisposable
    {
        
        void CreateProduct();
        List<string> GetDestinations();
        List<string> GetThemes();

       // void ModifyProduct();


        //etc...
    }
}
