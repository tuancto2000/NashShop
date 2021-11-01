using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public double SubTotal { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
