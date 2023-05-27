using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; } = null!;
    }
}