using NashShop_ViewModel.Categories;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Models
{
    public class HomeVM
    {
        public List<ProductVM> FeaturedProducts { get; set; }
        public List<CategoryVM> Categories { get; set; }


    }
}
