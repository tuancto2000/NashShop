using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAll();
        Task<CategoryVM> GetById(int id);
        Task<bool> Update(CategoryUpdateRequest request);
        Task<bool> Create(CategoryCreateRequest request);
        Task<bool> Delete(int id);

    }
}
