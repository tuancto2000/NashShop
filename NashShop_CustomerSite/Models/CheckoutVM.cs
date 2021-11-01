using NashShop_ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Models
{
    public class CheckoutVM
    {
        public CartVM Cart { get; set; }

        public CheckoutRequest CheckoutModel { get; set; }
    }
}
