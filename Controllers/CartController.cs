using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Helper;
using TripMeOn.Models.Order;
using TripMeOn.Models.Products;

namespace TripMeOn.Controllers
{
   
	public class CartController : Controller
	{
		private readonly IOrderService _orderService;

		public CartController(IOrderService orderService)
		{
			_orderService = orderService;
		}
        

		public IActionResult ViewCart()
		{
			var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
			Cart cart;
			if (cartId != 0)
			{
				cart = _orderService.GetCart(cartId);
			}
			else
			{
				cart = new Cart() { Items = new List<Item>() };
			}
			return View(cart);
		}
      
        public IActionResult AddToCart(int tourPackageId, int quantity)
        {

            return BuyProduct(tourPackageId, quantity);
        }

        public IActionResult BuyProduct(int tourPackageId, int quantity)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");

            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();
                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity });
            }

            return RedirectToAction("ViewCart", "Cart"); // Redirect to the cart page after adding the item
        }

        public IActionResult RemoveProduct(int id)
		{
			var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
			_orderService.RemoveItem(cartId, id);

			return RedirectToAction("ViewCart");
		}
		// return item.id if product is already in item 
		// return -1 otherwise
		private int ProductExistInCart(Cart cart, int tourPackageId)
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

        [HttpPost]
        public IActionResult UpdateQuantity(int itemId, int quantity)
        {
            _orderService.UpdateItemQuantity(itemId, quantity);

            return RedirectToAction("ViewCart");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CheckoutForm()
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            var cart = _orderService.GetCart(cartId);
            var clientId = Convert.ToInt32(User.Identity.Name);//

            if (cart == null)
            {
                return NotFound();
            }       
            
            return View("CheckoutForm", cart);
        }

        public IActionResult PurchaseSuccess()
        {
            return View();
        }

    }
}
