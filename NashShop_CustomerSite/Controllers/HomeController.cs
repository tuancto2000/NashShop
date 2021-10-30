using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NashShop_CustomerSite.Interfaces;
using NashShop_CustomerSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger, ICategoryApiClient categoryApiClient, 
            IProductApiClient productApiClient)
        {
            _logger = logger;
            _categoryApiClient = categoryApiClient;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryApiClient.GetAll();
            var featuredProducts = await _productApiClient.GetFeaturedProducts(10);
            var model = new HomeVM
            {
                Categories = categories,
                FeaturedProducts = featuredProducts
            };
            return View(model);        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
