using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Products
{
    public class ProductCreateRequest
    {
        public double Price { set; get; }
        [Required(ErrorMessage = "Bạn phải nhập tên sản phẩm")]
        public string Name { set; get; }
        public string Description { set; get; }
        [Required]
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
