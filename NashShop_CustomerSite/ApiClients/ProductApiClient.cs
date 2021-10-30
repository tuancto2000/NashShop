using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NashShop_CustomerSite.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClients
{
    public class ProductApiClient : BaseApiClient,IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
            : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<PagedResult<ProductVM>> GetAllPaging(PagingRequest request)
        {

            var client = this.CreateClient();
            var url = $"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}";
            return await this.GetAsync<PagedResult<ProductVM>>(url, client);
        }

        public async Task<ProductVM> GetById(int productId)
        {
            var client = this.CreateClient();
            var url = $"/api/products/{productId}";
            return await this.GetAsync<ProductVM>(url, client);
        }

        public async Task<List<ProductVM>> GetFeaturedProducts(int take)
        {
            var client = this.CreateClient();
            var url = $"/api/products/featured/{take}";
            return await this.GetAsync<List<ProductVM>>(url, client);
        }
        public async Task<PagedResult<ProductVM>> GetByCategoryId(PagingRequest request, int categoryId)
        {
            var client = this.CreateClient();
            var url = $"/api/products/paging/{categoryId}?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}";
            return await this.GetAsync<PagedResult<ProductVM>>(url, client);
        }

        public async Task<List<ProductImageVM>> GetProductImages(int productId)
        {
            var client = this.CreateClient();
            var url = $"/api/products/{productId}/images";
            return await this.GetAsync<List<ProductImageVM>>(url, client);
        }
        public async Task<bool> AddRating(ProductRatingRequest request)
        {
            var client = this.CreateClient();
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/api/products/rating", httpContent);

            return response.IsSuccessStatusCode;
        }

        public Task<bool> UpdateRating(ProductRatingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
