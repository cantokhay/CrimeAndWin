using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Domain.VOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> b)
        {
            b.ToTable("Items");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.Property(x => x.Quantity).HasDefaultValue(1);

            // Complex Property: Value
            b.ComplexProperty(x => x.Value, v =>
            {
                v.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2).HasDefaultValue(0);
                v.Property(p => p.Currency).HasColumnName("CurrencyType").IsRequired();
            });

            // Complex Property: Stats
            b.ComplexProperty(x => x.Stats, s =>
            {
                s.Property(p => p.Damage).HasColumnName("Damage").HasDefaultValue(0);
                s.Property(p => p.Defense).HasColumnName("Defense").HasDefaultValue(0);
                s.Property(p => p.Power).HasColumnName("Power").HasDefaultValue(0);
            });

            b.HasOne(x => x.Inventory)
             .WithMany(i => i.Items)
             .HasForeignKey(x => x.InventoryId)
             .OnDelete(DeleteBehavior.Restrict);

            // Seeding Items
            b.HasData(
                new Item 
                { 
                    Id = SeedDataConstants.ItemDesertEagleId, 
                    InventoryId = SeedDataConstants.InventoryAlphaId, 
                    Name = "Desert Eagle .50 AE", 
                    Quantity = 1,
                    Stats = new ItemStats(85, 0, 20),
                    Value = new ItemValue(15000.00m, CurrencyType.Gold),
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Item 
                { 
                    Id = SeedDataConstants.ItemKevlarVestId, 
                    InventoryId = SeedDataConstants.InventoryAlphaId, 
                    Name = "Kevlar Tactical Vest", 
                    Quantity = 1,
                    Stats = new ItemStats(0, 70, 5),
                    Value = new ItemValue(8500.00m, CurrencyType.Gold),
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Item 
                { 
                    Id = SeedDataConstants.ItemAdrenalineId, 
                    InventoryId = SeedDataConstants.InventoryBetaId, 
                    Name = "Adrenaline Shot", 
                    Quantity = 5,
                    Stats = new ItemStats(0, 0, 15),
                    Value = new ItemValue(250.00m, CurrencyType.Gem),
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}
