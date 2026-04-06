using Shared.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using GameWorld.Domain.Entities;

namespace GameWorld.Infrastructure.Persistance.Context
{
    public class GameWorldDbContext : DbContext
    {
        public GameWorldDbContext(DbContextOptions<GameWorldDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.GameWorld> GameWorlds => Set<Domain.Entities.GameWorld>();
        public DbSet<Domain.Entities.Season> Seasons => Set<Domain.Entities.Season>();
        public DbSet<Domain.Entities.District> Districts => Set<Domain.Entities.District>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameWorldDbContext).Assembly);
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
