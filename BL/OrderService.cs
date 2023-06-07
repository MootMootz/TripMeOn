using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL.interfaces;
using TripMeOn.Models;
using TripMeOn.Models.Order;

namespace TripMeOn.BL
{
    public class OrderService : IOrderService
    {
        private readonly Models.BddContext _bddContext;

        public OrderService()
        {
            _bddContext = new Models.BddContext();
        }

        public int CreateCart()
        {
            Cart cart = new Cart() { Items = new List<Item>() };
            _bddContext.Carts.Add(cart);
            _bddContext.SaveChanges();
            return cart.Id;
        }
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

        //public Cart GetCart(int cartId, int? clientId = null)
        //{
        //    var query = _bddContext.Carts
        //        .Include(c => c.Client)
        //        .Include(c => c.Items)
        //            .ThenInclude(it => it.TourPackage)
        //                .ThenInclude(tp => tp.Destination)
        //                    .ThenInclude(d => d.Country)
        //        .Include(c => c.Items)
        //            .ThenInclude(it => it.TourPackage)
        //                .ThenInclude(tp => tp.Destination)
        //                    .ThenInclude(d => d.Region)
        //        .Include(c => c.Items)
        //            .ThenInclude(it => it.TourPackage)
        //                .ThenInclude(tp => tp.Destination)
        //                    .ThenInclude(d => d.City)
        //        .Include(c => c.Items)
        //            .ThenInclude(it => it.TourPackage)
        //                .ThenInclude(tp => tp.Image)
        //        .Where(c => c.Id == cartId);
        //    if (clientId.HasValue)
        //    {
        //        query = query.Where(cl => cl.ClientId == clientId.Value);
        //    }
        //    return query.FirstOrDefault();
        //}


        public Cart GetCartWithClient(int cartId, int clientId)
        {
            var query = _bddContext.Carts
                .Include(c => c.Items)
                .ThenInclude(it => it.TourPackage)
                .Where(c => c.Id == cartId && c.ClientId == clientId);

            return query.FirstOrDefault();
        }

        public void UpdateCartClient(int cartId, int clientId)
        {
            var cart = _bddContext.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cart != null)
            {
                cart.ClientId = clientId;
                _bddContext.SaveChanges();
            }
        }


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

                // Check if the item already exists in the cart
                var existingItem = cart.Items.FirstOrDefault(i => i.TourPackageId == item.TourPackageId);
                var existingItemAccomodation = cart.Items.FirstOrDefault(a => a.AccomodationId == item.AccomodationId);

                if (existingItem != null)
                {
                    // Item already exists, update the quantity
                    existingItem.Quantity += item.Quantity;
                }

                if (existingItemAccomodation != null)
                {
                    // Item already exists, update the quantity
                    existingItemAccomodation.Quantity += item.Quantity;
                }
                else
                {
                    // Item does not exist, add it to the cart
                    cart.Items.Add(item);
                }

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