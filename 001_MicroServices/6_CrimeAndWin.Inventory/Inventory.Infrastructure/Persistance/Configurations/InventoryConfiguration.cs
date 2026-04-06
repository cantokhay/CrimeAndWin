using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Inventory> b)
        {
            b.ToTable("Inventories");
            b.HasKey(x => x.Id);

            b.Property(x => x.PlayerId).IsRequired();

            // Seed Inventories
            b.HasData(
                new Domain.Entities.Inventory 
                { 
                    Id = SeedDataConstants.InventoryAlphaId, 
                    PlayerId = SeedDataConstants.PlayerAlphaId, 
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Domain.Entities.Inventory 
                { 
                    Id = SeedDataConstants.InventoryBetaId, 
                    PlayerId = SeedDataConstants.PlayerBetaId, 
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}
