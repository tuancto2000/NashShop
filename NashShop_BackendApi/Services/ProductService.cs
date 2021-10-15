using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
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

        public async Task<int> AddImage(int productId, IFormFile ImageFile)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            if (ImageFile != null)
            {
                product.ImagePath = await this.SaveFile(ImageFile);
                
            }
            _context.Products.Update(product);
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
                Description = request.Description
            };
            if (request.Image != null)
            {
                product.ImagePath = await this.SaveFile(request.Image);
            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(ProductDeleteRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
                throw new Exception($"Cannot find an product with id {request.Id}");
            if (product.ImagePath != null)
            {
                await _storageService.DeleteFileAsync(product.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductVM> GetById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            var category = await _context.Categories.FindAsync(product.CategotyId);
            var productViewModel = new ProductVM()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = category.Name,
                ViewCount = product.ViewCount,
                ImagePath = product.ImagePath
            };
            return productViewModel;

        }

        public async Task<int> RemoveImage(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            if (product.ImagePath != null)
            {
                await _storageService.DeleteFileAsync(product.ImagePath);
                product.ImagePath = null;
               
            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
                throw new Exception($"Cannot find an product with id {request.Id}");

            product.Name = request.Name;
            product.Description = request.Description;

            if(request.Image!=null)
            {
                await _storageService.DeleteFileAsync(product.ImagePath);
                product.ImagePath = await SaveFile(request.Image);
               
            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
         
        }

        public async Task<int> UpdateImage(int productId, IFormFile ImageFile)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            if (ImageFile != null)
            {
                await _storageService.DeleteFileAsync(product.ImagePath);
                product.ImagePath = await this.SaveFile(ImageFile);
               
            }
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePrice(int productId, int newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception($"Cannot find an product with id {productId}");
            if (newPrice >= 0)
            {
                product.Price = newPrice;
          
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
                        select new { p, c };
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
                    ImagePath = x.p.ImagePath
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
    }
}
