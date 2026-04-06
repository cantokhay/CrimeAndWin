using GameWorld.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace GameWorld.Infrastructure.Persistance.Configuration
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> b)
        {
            b.ToTable("Districts");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.Property(x => x.TaxRate).HasPrecision(5, 2).HasDefaultValue(0.05m);
            b.Property(x => x.TotalRespectPoints).HasPrecision(18, 2).HasDefaultValue(0);

            // Seed Districts
            b.HasData(
                new District 
                { 
                    Id = SeedDataConstants.RegionAlcatrazId, 
                    Name = "Alcatraz Maximum Security", 
                    Description = "High risk, high reward.",
                    TaxRate = 0.15m,
                    TotalRespectPoints = 0,
                    CreatedAtUtc = DateTime.UtcNow 
                },
                new District 
                { 
                    Id = SeedDataConstants.RegionDowntownId, 
                    Name = "Downtown Metropolis", 
                    Description = "The center of the city.",
                    TaxRate = 0.05m,
                    TotalRespectPoints = 0,
                    CreatedAtUtc = DateTime.UtcNow 
                }
            );
        }
    }
}
