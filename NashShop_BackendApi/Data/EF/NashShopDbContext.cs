using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NashShop_BackendApi.Data.Configurations;
using NashShop_BackendApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NashShop_BackendApi.Data.EF
{
    public class NashShopDbContext : DbContext
    {
        public NashShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
    public class NashShopDbContextFactory : IDesignTimeDbContextFactory<NashShopDbContext>
    {
        private const string ConnectString = "Server=.;Database=NashShop;Trusted_Connection=True;";
        public NashShopDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NashShopDbContext>();
            //var connectionString = configuration.GetConnectionString(ConnectString);
            builder.UseSqlServer(ConnectString);
            return new NashShopDbContext(builder.Options);
        }
    }
}
