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
            b.Property(x => x.BaseTaxRate).HasPrecision(5, 2).HasDefaultValue(5.0);
            b.Property(x => x.MinBusinessRespect).HasDefaultValue(0);

            // Seed Districts
            b.HasData(
                new District 
                { 
                    Id = SeedDataConstants.RegionAlcatrazId, 
                    Name = "Alcatraz Maximum Security", 
                    BaseTaxRate = 15.0m,
                    MinBusinessRespect = 1000,
                    CreatedAtUtc = DateTime.UtcNow 
                },
                new District 
                { 
                    Id = SeedDataConstants.RegionDowntownId, 
                    Name = "Downtown Metropolis", 
                    BaseTaxRate = 5.0m,
                    MinBusinessRespect = 500,
                    CreatedAtUtc = DateTime.UtcNow 
                }
            );
        }
    }
}
