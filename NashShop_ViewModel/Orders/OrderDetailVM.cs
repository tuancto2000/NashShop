using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Orders
{
    public class OrderDetailVm
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
