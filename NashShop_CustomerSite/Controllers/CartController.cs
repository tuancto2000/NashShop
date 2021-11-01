using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NashShop_CustomerSite.Interfaces;
using NashShop_CustomerSite.Models;
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
        private readonly string _cartSession = "CartSession";

        public CartController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
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

            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            int quantity = 1;
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
                    Quantity = quantity
                };

                currentCart.Add(cartItem);
            }

            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return Ok(currentCart);
        }
        public IActionResult RemoveItem(int id)
        {
            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            var item = currentCart.FirstOrDefault(x => x.ProductId == id);
            if (item!=null)
            {
                currentCart.Remove(item);
            }
            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return RedirectToAction("Index", "Cart");
        }
        public async Task<IActionResult> Checkout(int id)
        {
            var product = await _productApiClient.GetById(id);

            var session = HttpContext.Session.GetString(_cartSession);
            List<CartItemVM> currentCart = new List<CartItemVM>();
            if (session != null)
                currentCart = JsonConvert.DeserializeObject<List<CartItemVM>>(session);
            int quantity = 1;
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
                    Quantity = quantity
                };

                currentCart.Add(cartItem);
            }

            HttpContext.Session.SetString(_cartSession, JsonConvert.SerializeObject(currentCart));
            return View();
        }
    }
}
