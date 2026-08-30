using Microsoft.EntityFrameworkCore;
using Notification.Notification.Domain.Models;

namespace Notification.Notification.Infrastructure.DbSettings
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
        typeof(NotificationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Notifications> Notifications { get; set; }
    }
}
