using NashShop_BackendApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Data.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public Guid UserId { set; get; }

        public OrderStatus Status { set; get; }
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public User User { get; set; }
    }
}
