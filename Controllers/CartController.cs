using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Helper;
using TripMeOn.Models.Order;
using TripMeOn.Models.Products;
using TripMeOn.ViewModels;

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

        //[Authorize]
        //[HttpGet]
        //public IActionResult CheckoutForm()
        //{
        //    var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
        //    var cart = _orderService.GetCart(cartId);
        //    var clientId = Convert.ToInt32(User.Identity.Name);

        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View("CheckoutForm", cart);
        //}

        [Authorize]
        [HttpGet]
        public IActionResult CheckoutForm()
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            var clientId = int.Parse(User.Identity.Name);

            var cart = _orderService.GetCartWithClient(cartId, clientId);

            if (cart == null)
            {
                return NotFound();
            }

            var viewModel = new CheckoutViewModel
            {
                Id = cart.Id,
                Items = cart.Items,
                ClientId = cart.ClientId,
                Client = cart.Client,
                IsRefunded = cart.IsRefunded,
                TourPackageId = cart.TourPackageId,
                Quantity = cart.Quantity
            };

            return View("CheckoutForm", viewModel);
        }





        public IActionResult PurchaseSuccess()
        {
            return View();

        }
        [Authorize]
        public IActionResult OrderStatus()
        {
            int clientId = Convert.ToInt32(User.Identity.Name);
            List<Cart> carts = _orderService.GetOrdersByUserId(clientId);

            List<OrderViewModel> orders = carts.Select(cart => new OrderViewModel
            {
                CartId = cart.Id,
                ClientId = (int)cart.ClientId,
                Items = cart.Items,
                IsRefunded = cart.IsRefunded,
                Quantity = cart.Items.Sum(item => item.Quantity),
                TotalPrice = cart.Items.Sum(item => item.Quantity * item.TourPackage.Price)
            }).ToList();

            // Store orders in session
            HttpContext.Session.SetObjectAsJson("Orders", orders);

            return View("OrderStatusView", orders);
        }



        public IActionResult OrderStatusView()
        {
            // Retrieve orders from session
            List<OrderViewModel> orders = HttpContext.Session.GetObjectFromJson<List<OrderViewModel>>("Orders");

            return View(orders);
        }


        [Authorize]
        public IActionResult RequestReturn(int cartId)
        {
            // Logic to handle the return request for the specified cartId
            // ...

            return RedirectToAction("OrderStatus");
        }
    }
}

    //[Authorize]
    //[HttpPost]
    //public IActionResult Refund(int cartId)
    //{
    //    int clientId = Convert.ToInt32(User.Identity.Name);

    //    // Get the cart for the specified cartId and clientId
    //    Cart cart = _orderService.GetCart(cartId, clientId);

    //    if (cart != null)
    //    {
    //        // Perform the refund logic here
    //        // ...

    //        // Update the cart or mark it as refunded in the database
    //        // ...

    //        return RedirectToAction("Refund");
    //    }

    //    // If the cart is not found, handle the error accordingly
    //    return NotFound();
    //}



    

