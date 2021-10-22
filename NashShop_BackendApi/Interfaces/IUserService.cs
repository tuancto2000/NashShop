using NashShop_ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        Task<bool> Register(RegisterRequest request);

    }
}
