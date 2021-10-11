using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
