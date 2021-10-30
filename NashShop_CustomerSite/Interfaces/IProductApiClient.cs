using NashShop_ViewModel;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Interfaces
{
    public interface IProductApiClient
    {

        Task<ProductVM> GetById(int productId);
        Task<PagedResult<ProductVM>> GetAllPaging(PagingRequest request);
        Task<List<ProductVM>> GetFeaturedProducts(int take);
        Task<bool> AddRating(ProductRatingRequest request);
        Task<bool> UpdateRating(ProductRatingRequest request);
        Task<List<ProductImageVM>> GetProductImages(int productId);
        Task<PagedResult<ProductVM>> GetByCategoryId(PagingRequest request, int categoryId);
    }
}
