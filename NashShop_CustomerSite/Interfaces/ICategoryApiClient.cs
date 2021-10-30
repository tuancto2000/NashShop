using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_CustomerSite.Interfaces
{
    public interface ICategoryApiClient
    {
        public Task<List<CategoryVM>> GetAll();
        public Task<CategoryVM> GetById(int categoryId);

    }
}
