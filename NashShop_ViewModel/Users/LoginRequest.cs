using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_ViewModel.Users
{
    public class LoginRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

    }
}
