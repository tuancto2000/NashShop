using NashShop_ViewModel.Categories;
using NashShop_ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Interfaces
{
    public interface ICartApiClient
    {
        Task<bool> Checkout(CheckoutRequest request);
    }
}
