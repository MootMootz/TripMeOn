using System;
using TripMeOn.Models.Order;

namespace TripMeOn.BL.interfaces
{
    public interface IOrderService : IDisposable
    {
        int CreateCart();

        Cart GetCart(int cartId);

        void AddItem(int CartId, Item item);

        void UpdateItemQuantity(int ItemId);
        void RemoveItem(int cartId, int itemId);
	}
}
