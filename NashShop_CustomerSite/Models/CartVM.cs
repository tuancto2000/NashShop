using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Models
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; }
        public double Total { get; set; }
    }
}
