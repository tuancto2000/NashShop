using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Data.Entities;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NashShopDbContext _context;
        private readonly IFileStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public CategoryService(NashShopDbContext context, IFileStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {

            var category = new Category()
            {
                Name = request.Name,
                DateCreated = DateTime.Now,
                Description = request.Description
            };
            if (request.Image != null)
            {
                category.ImagePath = await this.SaveFile(request.Image);
            }
            _context.Categories.Add(category);
             await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new Exception($"Cannot find a category with id {id}");
            await _storageService.DeleteFileAsync(category.ImagePath);

            _context.Categories.Remove(category);

            return await _context.SaveChangesAsync()>0;
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            var query = from c in _context.Categories
                        select c;
            return await query.Select(x => new CategoryVM()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.ImagePath,
                Description = x.Description,
                DateCreated =x.DateCreated,
                DateUpdated = x.DateUpdated
               
            }).ToListAsync();
        }
        public async Task<CategoryVM> GetById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
                throw new Exception($"Cannot find a category with id {categoryId}");
            return new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.ImagePath,
                Description = category.Description
            };

        }

        public async Task<bool> Update(CategoryUpdateRequest request)
        {

           var category = await _context.Categories.FindAsync(request.Id);
            if (category == null)
                throw new Exception($"Cannot find a category with id {request.Id}");

            category.Name = request.Name;
            category.Description = request.Description;
            category.DateUpdated = DateTime.Now;

            if (request.Image != null)
            {
                category.ImagePath = await this.SaveFile(request.Image);

            }
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }

}
