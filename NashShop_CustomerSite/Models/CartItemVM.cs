using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Models
{
    public class CartItemVM
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }


    }
}
