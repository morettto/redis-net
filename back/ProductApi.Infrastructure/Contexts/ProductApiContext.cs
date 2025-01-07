using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Models.Products;

namespace ProductApi.Infrastructure.Contexts
{
    public class ProductApiContext : DbContext
    {
        public ProductApiContext(DbContextOptions<ProductApiContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(t => t.Id);
            builder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
        }
    }
}
