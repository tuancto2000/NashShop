using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NashShop_CustomerSite.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Orders;
using NashShop_ViewModel.Shared;
using NashShop_ViewModel.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClients
{
    public class CartApiClient : BaseApiClient,ICartApiClient
    {
        public CartApiClient(IHttpClientFactory httpClientFactory,
              IHttpContextAccessor httpContextAccessor, IConfiguration configuration) :
              base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<bool> Checkout (CheckoutRequest request)
        {
            var client = this.CreateClient();

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/orders/checkout", httpContent);

            return response.IsSuccessStatusCode;
        }

    }
}
