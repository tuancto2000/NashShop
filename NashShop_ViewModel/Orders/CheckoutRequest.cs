using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Orders
{
    public class CheckoutRequest
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Note { get; set; }

        public List<OrderDetailVm> OrderDetails { set; get; } = new List<OrderDetailVm>();
    }
}
