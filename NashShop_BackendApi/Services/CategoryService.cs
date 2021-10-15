using Microsoft.EntityFrameworkCore;
using NashShop_BackendApi.Data.EF;
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
                Name = x.Name
            }).ToListAsync();
        }
    }
}
