
using Identity.Identity.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Module.Identity.Infrastructure.DbSettings
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
                  : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(IdentityDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<GeneratedOTP> GeneratedOTP { get; set; }
    }
}
