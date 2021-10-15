﻿using NashShop_ViewModel.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetAll();
    }
}
