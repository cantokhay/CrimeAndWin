using Action.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Action.Infrastructure.Persistance.Context
{
    public class ActionDbContext : DbContext
    {
        public ActionDbContext(DbContextOptions<ActionDbContext> options) : base(options) { }

        public DbSet<ActionDefinition> ActionDefinitions => Set<ActionDefinition>();
        public DbSet<PlayerActionAttempt> PlayerActionAttempts => Set<PlayerActionAttempt>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.ApplyConfigurationsFromAssembly(typeof(ActionDbContext).Assembly);
        }
    }
}
