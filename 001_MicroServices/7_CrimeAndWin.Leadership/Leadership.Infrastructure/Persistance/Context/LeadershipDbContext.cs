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
        }
    }
}
