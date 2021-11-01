using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NashShop_CustomerSite.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Categories;
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
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory,
           IHttpContextAccessor httpContextAccessor, IConfiguration configuration) :
           base(httpClientFactory, httpContextAccessor, configuration)
        {
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            // add 
            var client = this.CreateClientWithoutToken();
            var url = "/api/categories";
            return await this.GetAsync<List<CategoryVM>>(url,client); 
        }
        public async Task<CategoryVM> GetById(int categoryId)
        {
            var client = this.CreateClientWithoutToken();
            var url = $"/api/categories/{categoryId}";
            return await this.GetAsync<CategoryVM>(url, client);
        }
    }
}
