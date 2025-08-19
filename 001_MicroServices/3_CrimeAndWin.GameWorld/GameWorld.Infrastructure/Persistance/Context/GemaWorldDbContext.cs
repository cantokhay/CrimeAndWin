using Microsoft.EntityFrameworkCore;
using GameWorld.Domain.Entities;

namespace GameWorld.Infrastructure.Persistance.Context
{
    public class GameWorldDbContext : DbContext
    {
        public GameWorldDbContext(DbContextOptions<GameWorldDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.GameWorld> GameWorlds => Set<Domain.Entities.GameWorld>();
        public DbSet<Season> Seasons => Set<Season>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameWorldDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
