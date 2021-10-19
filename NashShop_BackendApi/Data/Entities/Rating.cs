using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Data.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
        public int Stars { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
