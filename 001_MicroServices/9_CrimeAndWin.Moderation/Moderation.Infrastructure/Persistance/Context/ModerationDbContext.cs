using Microsoft.EntityFrameworkCore;
using Moderation.Domain.Entities;
using System.Reflection;

namespace Moderation.Infrastructure.Persistance.Context
{
    public class ModerationDbContext : DbContext
    {
        public ModerationDbContext(DbContextOptions<ModerationDbContext> options) : base(options) { }

        public DbSet<Report> Reports => Set<Report>();
        public DbSet<ModerationAction> ModerationActions => Set<ModerationAction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
