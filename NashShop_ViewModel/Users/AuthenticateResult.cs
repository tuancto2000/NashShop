using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Users
{
    public class AuthenticateResult
    {
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}
