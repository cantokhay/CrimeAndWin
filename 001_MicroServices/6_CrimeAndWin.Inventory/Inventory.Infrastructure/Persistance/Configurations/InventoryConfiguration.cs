using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public sealed class InventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Inventory> b)
        {
            b.ToTable("Inventories");
            b.HasKey(x => x.Id);
            b.Property(x => x.PlayerId).IsRequired();
            b.Property(x => x.CreatedAtUtc).IsRequired();
            b.HasMany(i => i.Items)
             .WithOne(i => i.Inventory)   // navigation property’yi belirt
             .HasForeignKey(i => i.InventoryId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
