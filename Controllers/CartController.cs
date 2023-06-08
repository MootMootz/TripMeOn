using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TripMeOn.BL;
using TripMeOn.BL.interfaces;
using TripMeOn.Helper;
using TripMeOn.Models;
using TripMeOn.Models.Order;
using TripMeOn.Models.PartnerProducts;
using TripMeOn.Models.Products;
using TripMeOn.ViewModels;


namespace TripMeOn.Controllers
{

    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        //private Models.BddContext _bddContext;


        public CartController(IOrderService orderService)
        {
            _orderService = orderService;
            //_bddContext = new BddContext();
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

        //public IActionResult AddToCart(int tourPackageId, int quantity)
        //{

        //    return BuyProduct(tourPackageId, quantity);
        //}



        [HttpPost]
        public IActionResult BuyProduct(int tourPackageId, int quantity, DateTime startDate, DateTime endDate)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");


            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();

                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity, StartDate = startDate, EndDate = endDate });


                if (User.Identity.IsAuthenticated)
                {
                    var clientId = int.Parse(User.Identity.Name);
                    _orderService.UpdateCartClient(cartId, clientId); // Associate the client ID with the cart
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { TourPackageId = tourPackageId, Quantity = quantity, StartDate = startDate, EndDate = endDate });
            }

            HttpContext.Response.Cookies.Append("CartId", cartId.ToString());

            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult BuyAccomodation(int accomodationId, int quantity)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");


            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();
                _orderService.AddItem(cartId, new Item { AccomodationId = accomodationId, Quantity = quantity });

                if (User.Identity.IsAuthenticated)
                {
                    var clientId = int.Parse(User.Identity.Name);
                    _orderService.UpdateCartClient(cartId, clientId); // Associate the client ID with the cart
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { AccomodationId = accomodationId, Quantity = quantity });
            }

            HttpContext.Response.Cookies.Append("CartId", cartId.ToString());

            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult BuyRestaurant(int restaurantId, int quantity)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");


            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();
                _orderService.AddItem(cartId, new Item { RestaurantId = restaurantId, Quantity = quantity });

                if (User.Identity.IsAuthenticated)
                {
                    var clientId = int.Parse(User.Identity.Name);
                    _orderService.UpdateCartClient(cartId, clientId); // Associate the client ID with the cart
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { RestaurantId = restaurantId, Quantity = quantity });
            }

            HttpContext.Response.Cookies.Append("CartId", cartId.ToString());

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult BuyTransport(int transportId, int quantity)
        {
            var cartId = SessionHelper.GetObjectFromJson<int>(HttpContext.Session, "cartId");


            if (cartId == 0)
            {
                cartId = _orderService.CreateCart();
                _orderService.AddItem(cartId, new Item { TransportId = transportId, Quantity = quantity });

                if (User.Identity.IsAuthenticated)
                {
                    var clientId = int.Parse(User.Identity.Name);
                    _orderService.UpdateCartClient(cartId, clientId); // Associate the client ID with the cart
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cartId", cartId);
            }
            else
            {
                _orderService.AddItem(cartId, new Item { TransportId = transportId, Quantity = quantity });
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

            if (cart.ClientId == null)
            {
                _orderService.UpdateCartClient(cartId, clientId);
                cart = _orderService.GetCart(cartId);
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
                AccomodationId = item.AccomodationId,
                RestaurantId = item.RestaurantId,
                TransportId = item.TransportId,
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
                    TourPackageId = item.TourPackage != null ? item.TourPackageId : null,
                    TourPackageName = item.TourPackage != null ? item.TourPackage.Name : string.Empty,
                    TourPackagePrice = item.TourPackage != null ? item.TourPackage.Price : 0.0,
                    ImageUrl = item.TourPackage != null && item.TourPackage.Image != null ? item.TourPackage.Image.Url : string.Empty,
                    AccomodationId = item.Accomodation != null ? item.AccomodationId : null,
                    AccomodationName = item.Accomodation != null ? item.Accomodation.Name : string.Empty,
                    AccomodationPrice = item.Accomodation != null ? item.Accomodation.Price : 0.0,
                    RestaurantId = item.Restaurant != null ? item.RestaurantId : null,
                    RestaurantName = item.Restaurant != null ? item.Restaurant.Name : string.Empty,
                    RestaurantPrice = item.Restaurant != null ? item.Restaurant.Price : 0.0,
                    TransportId = item.Transportation != null ? item.TransportId : null,
                    TransportationType = item.Transportation != null ? item.Transportation.Type : string.Empty,
                    TransportationPrice = item.Transportation != null ? item.Transportation.Price : 0.0,
                }).ToList() : new List<ItemViewModel>(),


                IsRefunded = cart.IsRefunded,
                Quantity = cart.Items != null ? cart.Items.Sum(item => item.Quantity) : 0,
                TotalPrice = cart.Items != null ? cart.Items.Sum(item => item.Quantity * (
                (item.TourPackage != null ? item.TourPackage.Price : 0.0) +
                (item.Accomodation != null ? item.Accomodation.Price : 0.0) +
                (item.Restaurant != null ? item.Restaurant.Price : 0.0) +
                (item.Transportation != null ? item.Transportation.Price : 0.0)
                )) : 0.0
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
                //Notification notification = new Notification()
                //{
                //    Message = $"A refund request has been initiated for cart {cartId} by client {User.Identity.Name}.",
                //    CreatedAt = DateTime.Now
                //};
                //_bddContext.Notifications.Add(notification);
                //_bddContext.SaveChanges();
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

