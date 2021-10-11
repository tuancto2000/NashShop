using NashShop_BackendApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Services
{
    public class ProductServices : IProductServices
    {
        public async Task<int> Create(Product request)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetById(int productId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(Product request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrice(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
