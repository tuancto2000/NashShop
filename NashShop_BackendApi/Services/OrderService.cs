using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly NashShopDbContext _context;

        public OrderService(NashShopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Checkout(CheckoutRequest request)
        {
            var orderDetails = new List<OrderDetail>();
            foreach (var item in request.OrderDetails)
            {
                orderDetails.Add(new OrderDetail()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    SubTotal = item.SubTotal 
                });
            }
            var order = new Order()
            {
                UserId = request.UserId,
                FullName = request.FullName,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Note = request.Note,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                OrderDetails = orderDetails
            };
             _context.Orders.Add(order);
            return await _context.SaveChangesAsync()>0;

        }
    }
}
