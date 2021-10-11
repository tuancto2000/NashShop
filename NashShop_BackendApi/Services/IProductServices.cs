
using NashShop_BackendApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public interface IProductServices
    {
       public Task<int> Create(Product request);
       public Task<int> Update(Product request);
       public Task<int> Delete(int productId);
       public Task<Product> GetById(int productId);
       public Task<bool> UpdatePrice(int productId);
    }
}
