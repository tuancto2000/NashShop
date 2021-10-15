using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }  
        [Required]
        public string Name { set; get; }
        [Required]
        public string Description { set; get; }
        public IFormFile Image { get; set; }
    }
}
