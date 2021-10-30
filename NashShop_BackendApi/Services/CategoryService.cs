using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
using NashShop_BackendApi.Interfaces;
using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NashShopDbContext _context;
        public CategoryService(NashShopDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            var query = from c in _context.Categories
                        select c;
            return await query.Select(x => new CategoryVM()
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.ImagePath
            }).ToListAsync();
        }
        public async Task<CategoryVM> GetById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return new CategoryVM()
            {
                Id = category.Id,
                Name = category.Name
            };

        }
    }
}
