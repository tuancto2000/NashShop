using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Products
{
    public class ProductRatingRequest
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public double Stars { get; set; }
    }
}
