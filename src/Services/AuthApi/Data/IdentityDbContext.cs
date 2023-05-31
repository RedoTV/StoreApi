using AuthApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Data
{
    public class IdentityDbContext : DbContext {
        public IdentityDbContext (DbContextOptions<IdentityDbContext> options) : base (options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}