using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NashShop_CustomerSite.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
