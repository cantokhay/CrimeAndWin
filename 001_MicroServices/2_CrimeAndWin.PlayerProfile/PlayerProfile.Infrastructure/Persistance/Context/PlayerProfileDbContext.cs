using Microsoft.EntityFrameworkCore;
using PlayerProfile.Domain.Entities;

namespace PlayerProfile.Infrastructure.Persistance.Context
{
    public sealed class PlayerProfileDbContext : DbContext
    {
        public PlayerProfileDbContext(DbContextOptions<PlayerProfileDbContext> opt) : base(opt) { }
        public DbSet<Player> Players => Set<Player>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Player>(cfg =>
            {
                cfg.ToTable("Players");
                cfg.HasKey(p => p.Id);

                cfg.Property(p => p.AppUserId).IsRequired();
                cfg.Property(p => p.DisplayName).HasMaxLength(30);
                cfg.Property(p => p.AvatarKey).HasMaxLength(64);

                // VO: Stats
                cfg.ComplexProperty(p => p.Stats, o =>
                {
                    o.Property(x => x.Power).HasColumnName("Power");
                    o.Property(x => x.Defense).HasColumnName("Defense");
                    o.Property(x => x.Agility).HasColumnName("Agility");
                    o.Property(x => x.Luck).HasColumnName("Luck");
                });

                // VO: Energy
                cfg.ComplexProperty(p => p.Energy, o =>
                {
                    o.Property(x => x.Current).HasColumnName("EnergyCurrent");
                    o.Property(x => x.Max).HasColumnName("EnergyMax");
                    o.Property(x => x.RegenPerMinute).HasColumnName("EnergyRegenPerMinute");
                });

                // VO: Rank
                cfg.ComplexProperty(p => p.Rank, o =>
                {
                    o.Property(x => x.RankPoints).HasColumnName("RankPoints");
                    o.Property(x => x.Position).HasColumnName("RankPosition");
                });
            });
        }
    }
}