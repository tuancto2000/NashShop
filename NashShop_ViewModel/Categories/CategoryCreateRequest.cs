using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Categories
{
    public class CategoryCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
