using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashShop_CustomerSite.Interfaces;
using NashShop_CustomerSite.Models;
using NashShop_ViewModel.Orders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICartApiClient _cartApiClient;
        private readonly string _cartSession = "CartSession";

        public CartController(IProductApiClient productApiClient, ICartApiClient cartApiClient)
        {
            _productApiClient = productApiClient;
            _cartApiClient = cartApiClient;
        }

        public IActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.Msg = TempData["ErrorMessage"];
            }
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var cartVM = new CartVM()
            {
                Items = currentCart,
                Total = Math.Round(currentCart.Sum(x => x.Price * x.Quantity), 2)
            };
            return View(cartVM);
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productApiClient.GetById(id);

            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            if (currentCart.Any(x => x.ProductId == id))
            {
                currentCart.First(x => x.ProductId == id).Quantity += 1;
            }
            else
            {
                var cartItem = new CartItemVM()
                {
                    ProductId = id,
                    Image = product.ImagePath,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                };

                currentCart.Add(cartItem);
            }

            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        public IActionResult RemoveItem(int id)
        {
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var item = currentCart.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                currentCart.Remove(item);
            }
            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return RedirectToAction("Index", "Cart");
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            var isAuth = User.Identity.IsAuthenticated;
            if (!isAuth)
            {
                TempData["ErrorMessage"] = "You must login to checkout";
                return RedirectToAction("Index", "Cart");
            }
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var cartVM = new CartVM()
            {
                Items = currentCart,
                Total = Math.Round(currentCart.Sum(x => x.Price * x.Quantity), 2)
            };
            var checkoutVm = new CheckoutVM()
            {
                Cart = cartVM,
                CheckoutModel = new CheckoutRequest()
            };
            return View(checkoutVm);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model)
        {
            var id = User.FindFirst("UserId")?.Value;
            if (id == null)
            {
                TempData["ErrorMessage"] = "You must login to checkout";
                return RedirectToAction("Index", "Cart");
            }
            List<CartItemVM> currentCart = new List<CartItemVM>();
            currentCart = GetCart();
            var orderDetails = new List<OrderDetailVm>();
            foreach (var item in currentCart)
            {
                orderDetails.Add(new OrderDetailVm()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SubTotal = Math.Round(item.Price*item.Quantity,2)
                });
            }
            var checkoutRequest = new CheckoutRequest()
            {
                Address = model.CheckoutModel.Address,
                FullName = model.CheckoutModel.FullName,
                Email = model.CheckoutModel.Email,
                PhoneNumber = model.CheckoutModel.PhoneNumber,
                OrderDetails = orderDetails,
                Note = model.CheckoutModel.Note
            };
            await _cartApiClient.Checkout(checkoutRequest);
            HttpContext.Session.Remove(_cartSession);
            return RedirectToAction("index", "home");
        }
        private List<CartItemVM> GetCart()
        {
            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            return currentCart;
        }

    }
}
