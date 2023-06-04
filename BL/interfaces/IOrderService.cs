﻿using System;
using System.Collections.Generic;
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
        List<Cart> GetOrdersByUserId(int clientId);
        Cart GetCartWithClient(int cartId, int clientId);
    }
}
