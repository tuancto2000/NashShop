using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using NashShop_CustomerSite.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Shared;
using NashShop_ViewModel.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClients
{
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var client = this.CreateClientWithoutToken();

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/users/login", httpContent);
            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var client = this.CreateClientWithoutToken();

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/users/register", httpContent);

            return response.IsSuccessStatusCode;
        }
    }
}
