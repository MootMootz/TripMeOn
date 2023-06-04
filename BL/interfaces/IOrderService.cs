using System;
using TripMeOn.Models.Order;

namespace TripMeOn.BL.interfaces
{
    public interface IOrderService : IDisposable
    {
        int CreateCart();

        Cart GetCart(int cartId, int? clientId = null);

        void AddItem(int CartId, Item item);
        void UpdateItemQuantity(int ItemId, int quantity);
        void RemoveItem(int cartId, int itemId);
        void ClearCart(int cartId);

    }
}
