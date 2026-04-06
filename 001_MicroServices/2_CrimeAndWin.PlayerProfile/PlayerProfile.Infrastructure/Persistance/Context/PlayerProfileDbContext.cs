using Shared.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PlayerProfile.Domain.Entities;
using CrimeAndWin.Shared.Constants;
using PlayerProfile.Domain.VOs;
using PlayerProfile.Domain.Enums;

namespace PlayerProfile.Infrastructure.Persistance.Context
{
    public sealed class PlayerProfileDbContext : DbContext
    {
        public PlayerProfileDbContext(DbContextOptions<PlayerProfileDbContext> opt) : base(opt) { }
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Gang> Gangs => Set<Gang>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Gang>(cfg =>
            {
                cfg.ToTable("Gangs");
                cfg.HasKey(g => g.Id);
                cfg.Property(g => g.Name).HasMaxLength(50).IsRequired();
                cfg.Property(g => g.Tag).HasMaxLength(5).IsRequired();
                cfg.Property(g => g.TotalRespectScore).HasPrecision(18, 2).HasDefaultValue(0);
                cfg.HasIndex(g => g.Tag).IsUnique();

                cfg.HasData(
                    new Gang 
                    { 
                        Id = SeedDataConstants.GangBloodlineId, 
                        Name = "The Bloodline", 
                        Tag = "BLD", 
                        TotalRespectScore = 5000, 
                        LeaderId = SeedDataConstants.PlayerAlphaId,
                        MemberCount = 2,
                        MaxMemberLimit = 20,
                        Level = 5,
                        IsActive = true,
                        CreatedAtUtc = SeedDataConstants.SeedDate,
                        IsDeleted = false
                    },
                    new Gang 
                    { 
                        Id = SeedDataConstants.GangSiliconId, 
                        Name = "Silicon Shadows", 
                        Tag = "SIL", 
                        TotalRespectScore = 2500, 
                        LeaderId = SeedDataConstants.PlayerBetaId,
                        MemberCount = 1,
                        MaxMemberLimit = 20,
                        Level = 3,
                        IsActive = true,
                        CreatedAtUtc = SeedDataConstants.SeedDate,
                        IsDeleted = false
                    }
                );
            });

            b.Entity<Player>(cfg =>
            {
                cfg.ToTable("Players");
                cfg.HasKey(p => p.Id);

                cfg.Property(p => p.AppUserId).IsRequired();
                cfg.Property(p => p.DisplayName).HasMaxLength(30);
                cfg.Property(p => p.AvatarKey).HasMaxLength(64);
                
                cfg.Property(p => p.HeatIndex).HasPrecision(18, 2).HasDefaultValue(0);
                cfg.Property(p => p.RespectScore).HasPrecision(18, 2).HasDefaultValue(0);

                // Gang Mapping
                cfg.Property(p => p.GangId).IsRequired(false);
                cfg.Property(p => p.GangRole).HasDefaultValue(GangRole.None);

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

                cfg.HasData(
                    new Player 
                    { 
                        Id = SeedDataConstants.PlayerAlphaId, 
                        AppUserId = SeedDataConstants.UserAlphaId, 
                        DisplayName = "Alpha", 
                        AvatarKey = "avatar_alpha",
                        HeatIndex = 15,
                        RespectScore = 2500,
                        GangId = SeedDataConstants.GangBloodlineId,
                        GangRole = GangRole.Leader,
                        Stats = new Stats(75, 60, 50, 20),
                        Energy = new Energy(100, 100, 2),
                        Rank = new Rank(5000, 1),
                        LastEnergyCalcUtc = SeedDataConstants.SeedDate,
                        CreatedAtUtc = SeedDataConstants.SeedDate,
                        IsDeleted = false
                    },
                    new Player 
                    { 
                        Id = SeedDataConstants.PlayerBetaId, 
                        AppUserId = SeedDataConstants.UserBetaId, 
                        DisplayName = "Beta", 
                        AvatarKey = "avatar_beta",
                        HeatIndex = 85,
                        RespectScore = 450,
                        GangId = SeedDataConstants.GangBloodlineId,
                        GangRole = GangRole.Member,
                        Stats = new Stats(30, 20, 40, 10),
                        Energy = new Energy(25, 100, 2),
                        Rank = new Rank(850, 42),
                        LastEnergyCalcUtc = SeedDataConstants.SeedDate,
                        CreatedAtUtc = SeedDataConstants.SeedDate,
                        IsDeleted = false
                    }
                );
            });

            // 1. Soft Delete Filter
            foreach (var entityType in b.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
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
