using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_ViewModel.ProductImages;
using NashShop_ViewModel.Products;
using NashShop_ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class ProductService : IProductService
    {
        private readonly NashShopDbContext _context;
        private readonly IFileStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductService(NashShopDbContext context, IFileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");

            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                ProductId = productId,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,

            };
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
            }
            _context.ProductImages.Add(productImage);
            
            return await _context.SaveChangesAsync();
        }

        public async Task AddViewcount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            product.ViewCount += 1;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                IsFeatured = request.IsFeatured,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CategotyId = request.CategoryId,
                ViewCount = 0

            };
            if (request.Image != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "",
                        DateCreated = DateTime.Now,
                        ImagePath = await this.SaveFile(request.Image),
                        IsDefault = true
                    }
                };
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<ProductVM> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            var image = await _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault == true).FirstOrDefaultAsync();
            var category = await _context.Categories.FindAsync(product.CategotyId);
            var productViewModel = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = category.Name,
                ViewCount = product.ViewCount,
                ImagePath = image != null ? image.ImagePath : "no-image.jpg",
                DateCreated = product.DateCreated,
                DateUpdated = product.DateUpdated
            };
            return productViewModel;

        }

        public async Task<int> RemoveImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new Exception($"Cannot find an image with id {image}");
            await _storageService.DeleteFileAsync(image.ImagePath);

            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
                throw new Exception($"Cannot find an product with id {request.Id}");

            product.Name = request.Name;
            product.Description = request.Description;
            product.IsFeatured = request.IsFeatured;
            product.DateUpdated = DateTime.Now;

            if (request.Image != null)
            {
                var image = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (image != null)
                {
                    image.ImagePath = await this.SaveFile(request.Image);
                    _context.ProductImages.Update(image);
                }

            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();

        }


        public async Task<int> UpdatePrice(int productId, double newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            if (newPrice >= 0)
            {
                product.Price = newPrice;
                product.DateUpdated = DateTime.Now;

            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<PagedResult<ProductVM>> GetAllPaging(ProductPagingRequest request)
        {
            //1. join product and category
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategotyId equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where pi.IsDefault == true
                        select new { p, c, pi };
            //2. Filter
            if (request.CategoryId != null)
                query = query.Where(x => x.p.CategotyId == request.CategoryId);

            //3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductVM()
                {
                    Name = x.p.Name,
                    Price = x.p.Price,
                    Description = x.p.Description,
                    Category = x.c.Name,
                    ViewCount = x.p.ViewCount,
                    ImagePath = x.pi.ImagePath 

                }).ToListAsync();

            var pagedResult = new PagedResult<ProductVM>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;

        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new Exception($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.Caption = request.Caption;
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.IsDefault = request.IsDefault;
                productImage.DateUpdated = DateTime.Now;

            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductVM>> GetFeaturedProducts(int take)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategotyId equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where pi.IsDefault == true
                        select new { p, c, pi };
            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                 .Select(x => new ProductVM()
                 {
                     Id = x.p.Id,
                     Name = x.p.Name,
                     Category = x.c.Name,
                     DateCreated = x.p.DateCreated,
                     Description = x.p.Description,
                     Price = x.p.Price,
                     ViewCount = x.p.ViewCount,
                     ImagePath = x.pi.ImagePath


                 }).ToListAsync();
            return data;
        }


        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new Exception($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId
            };
            return viewModel;
        }
    }

}
