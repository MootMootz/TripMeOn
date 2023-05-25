using System.Collections.Generic;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Products;
using System.Linq;

namespace TripMeOn.BL
{
    public class ProductService //: IProductService
    {
        private BddContext _bddContext;

        public ProductService()
        {
            _bddContext = new BddContext();
        }
       

        public List<string> GetDestinations()
        {
            List<string> destinations = new List<string>();

            using (var dbContext = new BddContext())
            {
                destinations = dbContext.Destinations.Select(d => d.Country).ToList();
            }

            return destinations;
        }


        public List<string> GetThemes()
        {
            List<string> themes = new List<string>();

            using (var dbContext = new BddContext())
            {
                themes = dbContext.Themes.Select(t => t.Name).ToList();
            }

            return themes;
        }

       

       
        // Create

        // Modify

        // Delete

        // etc...
    }
}
