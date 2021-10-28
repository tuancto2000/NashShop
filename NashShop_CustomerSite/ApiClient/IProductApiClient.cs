using NashShop_ViewModel;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.ApiClient
{
    public interface IProductApiClient
    {
        Task<bool> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int productId);
        Task<ProductVM> GetById(int productId);
        Task<int> UpdatePrice(int productId, double newPrice);
        Task AddViewcount(int productId);
        Task<int> AddImage(int productId, ProductImageCreateRequest image);

        Task<int> RemoveImage(int productId);
        Task<PagedResult<ProductVM>> GetAllPaging(PagingRequest request);

        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<List<ProductVM>> GetFeaturedProducts(int take);
        Task<ProductImageViewModel> GetImageById(int imageId);
    }
}
