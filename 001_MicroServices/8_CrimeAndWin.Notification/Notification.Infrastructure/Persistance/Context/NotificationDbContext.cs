using Microsoft.EntityFrameworkCore;
using Notification.Infrastructure.Persistance.Configurations;

namespace Notification.Infrastructure.Persistance.Context
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
