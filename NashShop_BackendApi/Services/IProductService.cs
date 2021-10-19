
using Microsoft.AspNetCore.Http;
using NashShop_BackendApi.Data.Entities;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<ProductVM> GetById(int productId);
        Task<int> UpdatePrice(int productId, double newPrice);
        Task AddViewcount(int productId);
        Task<int> AddImage(int productId, ProductImageCreateRequest image);
        Task<int> RemoveImage(int imageId);
        Task<PagedResult<ProductVM>> GetAllPaging(ProductPagingRequest request);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<List<ProductVM>> GetFeaturedProducts( int take);
    }
}
