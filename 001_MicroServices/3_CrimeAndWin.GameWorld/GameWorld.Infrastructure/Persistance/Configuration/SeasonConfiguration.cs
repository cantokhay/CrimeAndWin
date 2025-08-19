using GameWorld.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameWorld.Infrastructure.Persistance.Configuration
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> b)
        {
            b.ToTable("Seasons");
            b.HasKey(x => x.Id);

            b.Property(x => x.SeasonNumber).IsRequired();

            b.OwnsOne(x => x.DateRange, dr =>
            {
                dr.Property(p => p.StartUtc).HasColumnName("StartUtc").IsRequired();
                dr.Property(p => p.EndUtc).HasColumnName("EndUtc").IsRequired();
            });

            b.Property(x => x.IsActive).HasDefaultValue(false);
        }
    }
}
