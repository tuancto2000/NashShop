using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.ProductImages
{
    public class ProductImageCreateRequest
    {
        public string Caption { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
