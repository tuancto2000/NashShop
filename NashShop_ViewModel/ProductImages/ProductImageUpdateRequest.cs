using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.ProductImages
{
    public class ProductImageUpdateRequest
    {

        public string Caption { get; set; }

        public IFormFile Image { get; set; }
    }
}
