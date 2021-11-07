using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NashShop_CustomerSite.Interfaces;
using NashShop_CustomerSite.Models;
using NashShop_ViewModel;
using NashShop_ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Detail(int id)
        {
            
            var product = await _productApiClient.GetById(id);
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.Msg = TempData["ErrorMessage"];
            }
            return View(product);
        }
        public async Task<IActionResult> Category(int id,int page = 1)
        {
            var paging = new PagingRequest()
            {
                PageIndex = page,
                PageSize = 2,
            };

            var products = await _productApiClient.GetByCategoryId(paging,id );
            return View(new ProductPagingVM()
            {
                Category = await _categoryApiClient.GetById(id),
                Products = products
            }); ;


        }
        public async Task<IActionResult> Rating(double star,int productId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var isAuth = User.Identity.IsAuthenticated;
            if (!isAuth)
            {
                TempData["ErrorMessage"] = "You must login to rate product";
                return RedirectToAction("Detail", "Product", new { id = productId });
            }
            var rating = new ProductRatingRequest()
            {
                ProductId = productId,
                Stars = star
            };
            await _productApiClient.AddRating(rating);
            return RedirectToAction("Detail", "Product", new { id = productId });
        }
    }
}
