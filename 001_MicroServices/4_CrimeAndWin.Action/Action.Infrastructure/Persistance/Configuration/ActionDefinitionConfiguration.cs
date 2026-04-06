using Action.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Action.Infrastructure.Persistance.Configuration
{
    public class ActionDefinitionConfiguration : IEntityTypeConfiguration<ActionDefinition>
    {
        public void Configure(EntityTypeBuilder<ActionDefinition> b)
        {
            b.ToTable("ActionDefinitions");
            b.HasKey(x => x.Id);

            b.Property(x => x.Code).HasMaxLength(32).IsRequired();
            b.Property(x => x.DisplayName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Description).HasMaxLength(500).IsRequired();
            
            // New Mechanics
            b.Property(x => x.Type).IsRequired();
            b.Property(x => x.BaseSuccessRate).HasPrecision(18, 2).HasDefaultValue(100);
            b.Property(x => x.HeatImpact).HasPrecision(18, 2).HasDefaultValue(0);
            b.Property(x => x.RespectImpact).HasPrecision(18, 2).HasDefaultValue(0);

            b.OwnsOne(x => x.Requirements, nb =>
            {
                nb.Property(p => p.MinPower).HasColumnName("MinPower").IsRequired();
                nb.Property(p => p.EnergyCost).HasColumnName("EnergyCost").IsRequired();
                nb.Property(p => p.DifficultyLevel).HasColumnName("DifficultyLevel").IsRequired();
            });

            b.OwnsOne(x => x.Rewards, nb =>
            {
                nb.Property(p => p.PowerGain).HasColumnName("PowerGain").IsRequired();
                nb.Property(p => p.ItemDrop).HasColumnName("ItemDrop").IsRequired();
                nb.Property(p => p.MoneyGain).HasColumnName("MoneyGain").HasPrecision(18, 2).IsRequired();
            });

            b.HasIndex(x => x.Code).IsUnique();
        }
    }
}
