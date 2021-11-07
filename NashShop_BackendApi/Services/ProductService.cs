using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel;
using NashShop_ViewModel.Categories;
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
            };
            if (request.Image!= null)
            {
                productImage.ImagePath = await this.SaveFile(request.Image);
            }
            _context.ProductImages.Add(productImage);

            await _context.SaveChangesAsync();
            return productImage.Id;
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
            _context.Products.Add(product);
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

            var images = _context.ProductImages.Where(x => x.ProductId == productId && x.IsDefault != true);

            var mainImage = await images.Where(x => x.IsDefault == true).FirstOrDefaultAsync();

            var category = await _context.Categories.FindAsync(product.CategotyId);

            var ratingCount = _context.Ratings.Where(x => x.ProductId == productId).Count();

            var productViewModel = new ProductVM()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImagePath = mainImage != null ? mainImage.ImagePath : "no-image.jpg",
                DateCreated = product.DateCreated,
                DateUpdated = product.DateUpdated,
                Star = product.Star,
                ProductCategory = new CategoryVM() { Id = category.Id, Name = category.Name },
                RatingCount = ratingCount,
                Images = images.Select(x => new ProductImageVM()
                {
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Caption = x.Caption,
                    DateCreated = x.DateCreated
                }).ToList()
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
            product.Price = request.Price;
            product.Description = request.Description;
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

        public async Task<PagedResult<ProductVM>> GetAllPaging(PagingRequest request)
        {
            //1. join product and category
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategotyId equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into pic
                        from pi in pic.DefaultIfEmpty()
                        where pi.IsDefault == true
                        select new { p, c, pi };

            //3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductVM()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    Description = x.p.Description,
                    ProductCategory = new CategoryVM() { Id = x.c.Id, Name = x.c.Name },
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
            productImage.Caption = request.Caption;
            if (request.Image != null)
            {

                productImage.ImagePath = await this.SaveFile(request.Image);

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
            var data = await query.OrderByDescending(x => x.p.Star).Take(take)
                 .Select(x => new ProductVM()
                 {
                     Id = x.p.Id,
                     Name = x.p.Name,
                     DateCreated = x.p.DateCreated,
                     Description = x.p.Description,
                     Price = x.p.Price,
                     ImagePath = x.pi.ImagePath,
                     Star = x.p.Star,
                     ProductCategory = new CategoryVM() { Id = x.c.Id, Name = x.c.Name }


                 }).ToListAsync();
            return data;
        }


        public async Task<ProductImageVM> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new Exception($"Cannot find an image with id {imageId}");
            var viewModel = new ProductImageVM()
            {
                Caption = image.Caption,
                Id = image.Id,
                ImagePath = image.ImagePath,
                ProductId = image.ProductId
            };
            return viewModel;
        }

        public async Task<bool> AddRating(ProductRatingRequest request)
        {
            var rating = new Rating()
            {
                UserId = request.UserId,
                Stars = request.Stars,
                ProductId = request.ProductId,
                DateCreated = DateTime.Now

            };
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            // update star
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {request.ProductId}");
            var avg = _context.Ratings
            .Where(r => r.ProductId == request.ProductId)
            .Average(r => r.Stars);
            if (Double.IsNaN(avg))
            {
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                product.Star = Math.Round(avg, 2);
                _context.Products.Update(product);
            }
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<List<ProductImageVM>> GetProductImages(int productId)
        {
            var data = await _context.ProductImages.Where(x => x.ProductId == productId)
                 .Select(x => new ProductImageVM()
                 {
                     Id = x.Id,
                     ProductId = x.ProductId,
                     ImagePath = x.ImagePath
                 }).ToListAsync();
            return data;
        }

        public async Task<PagedResult<ProductVM>> GetByCategoryId(PagingRequest request, int categoryId)
        {
            //1. join product and category
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategotyId equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into pic
                        from pi in pic.DefaultIfEmpty()
                        where pi.IsDefault == true
                        select new { p, c, pi };
            //2. filter
            query = query.Where(x => x.p.CategotyId == categoryId);
            //3.Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductVM()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    Description = x.p.Description,
                    ProductCategory = new CategoryVM() { Id = x.c.Id, Name = x.c.Name },

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

        public async Task<List<ProductVM>> GetAll()
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategotyId equals c.Id
                        join pi in _context.ProductImages on p.Id equals pi.ProductId
                        where pi.IsDefault == true
                        select new { p, c, pi };
            var data = await query
                 .Select(x => new ProductVM()
                 {
                     Id = x.p.Id,
                     Name = x.p.Name,
                     DateCreated = x.p.DateCreated,
                     DateUpdated = x.p.DateUpdated,
                     Description = x.p.Description,
                     Price = x.p.Price,
                     ImagePath = x.pi.ImagePath,
                     Star = x.p.Star,
                     ProductCategory = new CategoryVM() { Id = x.c.Id, Name = x.c.Name }


                 }).ToListAsync();
            return data;
        }
    }

}
