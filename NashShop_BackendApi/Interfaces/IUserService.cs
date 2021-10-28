using NashShop_ViewModel;
using NashShop_ViewModel.Shared;
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
        Task<PagedResult<UserVM>> GetUsersPaging(PagingRequest request);
    }
}
