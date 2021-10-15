
using Microsoft.AspNetCore.Http;
using NashShop_BackendApi.Data.Entities;
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
        Task<int> Delete(ProductDeleteRequest request);
        Task<ProductVM> GetById(int productId);
        Task<int> UpdatePrice(int productId, int newPrice);
        Task AddViewcount(int productId);
        Task<int> AddImage(int productId, IFormFile ImageFile);

        Task<int> RemoveImage(int productId);
        Task<PagedResult<ProductVM>> GetAllPaging(ProductPagingRequest request);

        Task<int> UpdateImage(int productId, IFormFile ImageFile);
    }
}
