using Microsoft.AspNetCore.Http;
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
        public string Category { get; set; }
        public int ViewCount { get; set; }
        public bool? IsFeatured { get; set; }
    }
}
