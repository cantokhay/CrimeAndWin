using Shared.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistance.Context
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<Domain.Entities.Inventory> Inventories => Set<Domain.Entities.Inventory>();
        public DbSet<Domain.Entities.Item> Items => Set<Domain.Entities.Item>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
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
