using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NashShop_ViewModel.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClient
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserApiClient(IHttpClientFactory httpClientFactory
)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5000");
            var response = await client.PostAsync("/api/users/login", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;

        }

        public async Task<string> Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
