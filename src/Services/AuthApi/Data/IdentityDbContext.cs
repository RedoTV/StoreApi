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
        protected override void OnModelCreating (ModelBuilder model)
        {
            model.Entity<User>().HasData( 
                new User
                {
                    Id = 1,
                    Name = "RedoTV",
                    HashedPassword = "94s8XADhQ+8PtV3uzcUVxaN4bY/btU2WAM7rxaTNXZE=",
                    Role = RolesEnum.Admin
                } 
            );
        }
    }
}