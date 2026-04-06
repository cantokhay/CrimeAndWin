using Action.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using System.Linq.Expressions;

namespace Action.Infrastructure.Persistance.Context
{
    public class ActionDbContext : DbContext
    {
        public ActionDbContext(DbContextOptions<ActionDbContext> options) : base(options) { }

        public DbSet<ActionDefinition> ActionDefinitions => Set<ActionDefinition>();
        public DbSet<PlayerActionAttempt> PlayerActionAttempts => Set<PlayerActionAttempt>();
        public DbSet<PlayerEnergyState> PlayerEnergyStates => Set<PlayerEnergyState>();
        public DbSet<GameSettings> GameSettings => Set<GameSettings>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.ApplyConfigurationsFromAssembly(typeof(ActionDbContext).Assembly);

            // 1. Soft Delete Filter
            foreach (var entityType in b.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var body = Expression.Not(property);
                    var lambda = Expression.Lambda(body, parameter);

                    b.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            // 2. Global OnDeleteBehavior (Restrict)
            foreach (var relationship in b.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
