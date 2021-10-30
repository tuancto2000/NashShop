using Microsoft.AspNetCore.Http;
using NashShop_ViewModel.Categories;
using NashShop_ViewModel.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Products
{
    public class ProductVM
    {
        public int Id { get; set; }
        public double Price { set; get; }
        public string Name { set; get; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { set; get; }
        public string? ImagePath { get; set; }
        public CategoryVM ProductCategory { get; set; }
        public double Star { get; set; }
        public int RatingCount { get; set; }
        public List<ProductImageVM> Images { get; set; }
    }
}

