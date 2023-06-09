﻿using System;
using System.Collections.Generic;
using TripMeOn.Models.Order;
using TripMeOn.Models.Users;

namespace TripMeOn.BL.interfaces
{
    /// <summary>
    /// L'interface OrderService sert à pouvoir remplir les articles choisis pour pouvoir les acheter
    /// </summary>
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
        void UpdateCartClient(int cartId, int clientId);
        int ProductExistInCart(Cart cart, int tourPackageId);

        void CreateRefundNotification(int cartId, Client client);
        //void AcceptRefundNotification(int cartId, Client client);
    }
}
