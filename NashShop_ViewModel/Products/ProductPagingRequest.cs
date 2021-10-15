using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Products
{
    public class ProductPagingRequest
    {
        public int PageIndex { get; set; }
        public int? CategoryId { get; set; }
        public int PageSize { get; set; }
    }
}
