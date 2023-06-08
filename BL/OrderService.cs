using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Order;

namespace TripMeOn.BL
{
    /// <summary>
    /// La BL OrderService permet de créér un pannier pour chaque utilisateur
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly Models.BddContext _bddContext;

        public OrderService()
        {
            _bddContext = new Models.BddContext();

		    }
        /// <summary>
        /// Méthode de création d'un pannier
        /// </summary>
        /// <returns></returns>

        


        public int CreateCart()
        {
            Cart cart = new Cart() { Items = new List<Item>() };
            _bddContext.Carts.Add(cart);
            _bddContext.SaveChanges();
            return cart.Id;
        }
        /// <summary>
        /// Méthode pour récuperer le pannier déjà créé avec les artlicles à l'intérieur
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public Cart GetCart(int cartId, int? clientId = null)
        {
            var query = _bddContext.Carts.Include(c => c.Client)
                .Include(c => c.Items).ThenInclude(it => it.TourPackage)
                 .Include(c => c.Items).ThenInclude(it => it.Accomodation)
                  .Include(c => c.Items).ThenInclude(it => it.Restaurant)
                   .Include(c => c.Items).ThenInclude(it => it.Transportation)
                .Where(c => c.Id == cartId);
            if (clientId.HasValue)
            {
                query = query.Where(cl => cl.ClientId == clientId.Value);
            }
            return query.FirstOrDefault();
        }

         public Cart GetCartWithClient(int cartId, int clientId)
        {
            var query = _bddContext.Carts
                .Include(c => c.Items)
                .ThenInclude(it => it.TourPackage)
                .Where(c => c.Id == cartId && c.ClientId == clientId);

            return query.FirstOrDefault();
        }
        /// <summary>
        /// Méthode pour modifier le pannier, ou mettre à jour
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="clientId"></param>
        public void UpdateCartClient(int cartId, int clientId)
        {
            var cart = _bddContext.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart != null)
            {
                cart.ClientId = clientId;
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Méthode pour rajouter un article
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="item"></param>
        public void AddItem(int cartId, Item item)
        {
            Cart cart = _bddContext.Carts.Include(c => c.Items).FirstOrDefault(c => c.Id == cartId);

            if (cart != null)
            {
                if (cart.Items == null)
                {
                    cart.Items = new List<Item>();
                }

                item.TourPackage = _bddContext.TourPackages.Find(item.TourPackageId);
                item.Accomodation = _bddContext.Accomodations.Find(item.AccomodationId);
                item.Restaurant = _bddContext.Restaurants.Find(item.RestaurantId);
                item.Transportation = _bddContext.Transportations.Find(item.TransportId);

                // Regarde si l'article existe déjà
                var existingItem = cart.Items.FirstOrDefault(i => i.TourPackageId == item.TourPackageId);
                var existingItemAccomodation = cart.Items.FirstOrDefault(a => a.AccomodationId == item.AccomodationId);
                var existingItemRestaurant = cart.Items.FirstOrDefault(r => r.RestaurantId == item.RestaurantId);
                var existingItemTransport = cart.Items.FirstOrDefault(t => t.TransportId == item.TransportId);

                if (existingItem != null)
                {
                    // l'article existe, donc on met à jour la quantité
                    existingItem.Quantity += item.Quantity;
                }

                if (existingItemAccomodation != null)
                {
                    
                    existingItemAccomodation.Quantity += item.Quantity;
                }

                if (existingItemRestaurant != null)
                {

                    existingItemRestaurant.Quantity += item.Quantity;
                }
                if (existingItemTransport != null)
                {

                    existingItemTransport.Quantity += item.Quantity;
                }
                else
                {
                    // s'il n'existe pas, on le rajoute au pannier
                    cart.Items.Add(item);
                }
                //on sauvegarde dans notre base de données
                _bddContext.SaveChanges();
            }
        }

        public void UpdateItemQuantity(int itemId, int quantity)
        {
            var item = _bddContext.Items.Find(itemId);
            if (item != null)
            {
                item.Quantity = quantity;
                _bddContext.SaveChanges();
            }
        }
        //méthode pour enlever un article du pannier
        public void RemoveItem(int cartId, int itemId)
        {
            Cart cart = GetCart(cartId);
            Item item = cart.Items.Where(it => it.Id == itemId).FirstOrDefault();

            cart.Items.Remove(item);

            _bddContext.SaveChanges();
        }

        public void ClearCart(int cartId)
        {
            // Retrieve the cart from the database
            Cart cart = _bddContext.Carts.FirstOrDefault(c => c.Id == cartId);

            if (cart != null)
            {
                // Remove all items from the cart
                cart.Items.Clear();

                // Save the changes to the database
                _bddContext.SaveChanges();
            }
        }
        public int ProductExistInCart(Cart cart, int tourPackageId)
        {
            foreach (var item in cart.Items)
            {
                if (item.TourPackageId == tourPackageId)
                {
                    return item.Id;
                }
            }
            return -1;
        }
        public List<Cart> GetOrdersByUserId(int clientId)
        {
            return _bddContext.Carts
                .Include(c => c.Items).ThenInclude(it => it.TourPackage)
                .Include(c => c.Client)
                .Where(c => c.ClientId == clientId)
                .ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}





//public void AddItem(int cartId, Item item)
//{
//    Cart cart = _bddContext.Carts.Find(cartId);
//    item.TourPackage = _bddContext.TourPackages.Find(item.TourPackageId);
//    cart.Items.Add(item);

//    _bddContext.SaveChanges();
//}
//public void AddItem(int cartId, Item item)
//{
//    Cart cart = _bddContext.Carts.Include(c => c.Items).FirstOrDefault(c => c.Id == cartId);

//    if (cart != null)
//    {
//        if (cart.Items == null)
//        {
//            cart.Items = new List<Item>();
//        }

//        item.TourPackage = _bddContext.TourPackages.Find(item.TourPackageId);
//        cart.Items.Add(item);

//        _bddContext.SaveChanges();
//    }
//}