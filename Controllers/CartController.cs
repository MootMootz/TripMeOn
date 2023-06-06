using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        
     
        [HttpPost]
        public IActionResult BuyProduct(int tourPackageId, int quantity)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
           

            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();
                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity });

                if (User.Identity.IsAuthenticated)
                {
                    var clientId = int.Parse(User.Identity.Name);
                    _orderService.UpdateCartClient(cartId, clientId); // Associate the client ID with the cart
                }
               
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity });
            }

            HttpContext.Response.Cookies.Append("CartId", cartId.ToString());

            return RedirectToAction("ViewCart");
        }


        public IActionResult RemoveProduct(int id)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");
            _orderService.RemoveItem(cartId, id);

            return RedirectToAction("ViewCart");
        }
        // return item.id if product is already in item 
        // return -1 otherwise
       

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
            var clientId = int.Parse(User.Identity.Name);

            var cart = _orderService.GetCart(cartId);

            if (cart == null)
            {
                return NotFound();
            }
            var item = cart.Items.FirstOrDefault();
            var viewModel = new CheckoutViewModel
            {
                Id = cart.Id,
                Items = cart.Items,
                ClientId = cart.ClientId,
                //Client = cart.Client,
                ClientName = cart.Client != null ? cart.Client.FirstName + " " + cart.Client.LastName : string.Empty,
                IsRefunded = cart.IsRefunded,
                TourPackageId = item.TourPackageId,
                Quantity = item.Quantity
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

            //var test = carts[0].Items.Sum(item => item.Quantity * (item.TourPackage != null && item.TourPackage.Price > 0.0 ? item.TourPackage.Price : 1.0));

            List<OrderViewModel> orders = carts.Select(cart => new OrderViewModel
            {
                CartId = cart.Id,
                ClientId = (int)cart.ClientId,
                ClientName = cart.Client != null ? cart.Client.FirstName + " " + cart.Client.LastName : string.Empty,

                Items = cart.Items != null ? cart.Items.Select(item => new ItemViewModel
                {
                    TourPackageId = item.TourPackageId,
                    TourPackageName = item.TourPackage != null ? item.TourPackage.Name : string.Empty,
                    TourPackagePrice = item.TourPackage != null ? item.TourPackage.Price : 0.0,
                    ImageUrl = item.TourPackage != null && item.TourPackage.Image != null ? item.TourPackage.Image.Url : string.Empty
                }).ToList() : new List<ItemViewModel>(),


                IsRefunded = cart.IsRefunded,
                Quantity = cart.Items != null ? cart.Items.Sum(item => item.Quantity) : 0,
                TotalPrice = cart.Items != null ? cart.Items.Sum(item => item.Quantity * (item.TourPackage != null && item.TourPackage.Price > 0.0 ? item.TourPackage.Price : 1.0)) : 0.0

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
        [HttpGet]
        public IActionResult Refund(int cartId)
        {
            // Retrieve the cart with the specified ID
            Cart cart = _orderService.GetCart(cartId);

            if (cart != null)
            {
                ViewData["RefundMessage"] = "Refund initiated successfully, it will be processed in 5 business days";
            }
            else
            {
                ViewData["RefundMessage"] = "Error: Cart not found.";
            }

            // Return the "Refund" view
            return View("Refund");
        }
    }
}

