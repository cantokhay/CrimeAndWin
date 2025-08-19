using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameWorld.Infrastructure.Persistance.Configuration
{
    public class GameWorldConfiguration : IEntityTypeConfiguration<Domain.Entities.GameWorld>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.GameWorld> b)
        {
            b.ToTable("GameWorlds");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            // GameRules owned VO
            b.OwnsOne(x => x.Rule, r =>
            {
                r.Property(p => p.MaxEnergy).HasColumnName("MaxEnergy").IsRequired();
                r.Property(p => p.RegenRatePerHour).HasColumnName("RegenRatePerHour").IsRequired();
            });

            b.HasMany(x => x.Seasons)
                .WithOne()
                .HasForeignKey(s => s.GameWorldId);
        }
    }
}
