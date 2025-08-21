using Inventory.Domain.Entities;
using Inventory.Domain.VOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistance.Configurations
{
    public sealed class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> b)
        {
            b.ToTable("Items");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Quantity).IsRequired();

            b.ComplexProperty(x => x.Stats, stats =>
            {
                stats.Property(p => p.Damage).HasColumnName("Damage").IsRequired();
                stats.Property(p => p.Defense).HasColumnName("Defense").IsRequired();
                stats.Property(p => p.Power).HasColumnName("Power").IsRequired();
            });

            b.ComplexProperty(x => x.Value, value =>
            {
                value.Property(p => p.Amount).HasColumnName("Amount").HasColumnType("decimal(18,2)").IsRequired();
                value.Property(p => p.Currency).HasColumnName("Currency").IsRequired();
            });
        }
    }
}
