using Microsoft.AspNetCore.Http;
using NashShop_ViewModel;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClient
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public Task<int> AddImage(int productId, ProductImageCreateRequest image)
        {
            throw new NotImplementedException();
        }

        public Task AddViewcount(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstants.AppSettings.Token);

            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5000");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.Image != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.Image.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.Image.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Description) ? "" : request.Description.ToString()), "description");
            var response = await client.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ProductVM>> GetAllPaging(PagingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ProductVM> GetById(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVM>> GetFeaturedProducts(int take)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageViewModel> GetImageById(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImage(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePrice(int productId, double newPrice)
        {
            throw new NotImplementedException();
        }

    }
}
