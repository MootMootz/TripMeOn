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
        private Models.BddContext _bddContext;

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
		public Cart GetCart(int cartId)
		{

			return _bddContext.Carts.Include(c => c.Items).ThenInclude(it => it.TourPackage).Where(c => c.Id == cartId).FirstOrDefault();
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

                // Check if the item already exists in the cart
                var existingItem = cart.Items.FirstOrDefault(i => i.TourPackageId == item.TourPackageId);

                if (existingItem != null)
                {
                    // Item already exists, update the quantity
                    existingItem.Quantity += item.Quantity;
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