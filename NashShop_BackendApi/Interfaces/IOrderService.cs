using NashShop_ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Interfaces
{
    public interface IOrderService
    {
        Task<bool> Checkout(CheckoutRequest request);
    }
}
