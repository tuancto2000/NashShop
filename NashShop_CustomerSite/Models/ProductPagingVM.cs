using NashShop_ViewModel.Categories;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Models
{
    public class ProductPagingVM
    {
        public CategoryVM Category { get; set; }
        public PagedResult<ProductVM> Products { get; set; }
    }
}
