using Shared.Domain;
using System.Linq.Expressions;
using Leadership.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leadership.Infrastructure.Persistance.Context
{
    public class LeadershipDbContext : DbContext
    {
        public LeadershipDbContext(DbContextOptions<LeadershipDbContext> options) : base(options) { }

        public DbSet<Leaderboard> Leaderboards => Set<Leaderboard>();
        public DbSet<LeaderboardEntry> LeaderboardEntries => Set<LeaderboardEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeadershipDbContext).Assembly);
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
