using Shared.Domain;
using System.Linq.Expressions;
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

            // 1. Soft Delete Filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var body = Expression.Not(property);
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            // 2. Global OnDeleteBehavior (Restrict)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
