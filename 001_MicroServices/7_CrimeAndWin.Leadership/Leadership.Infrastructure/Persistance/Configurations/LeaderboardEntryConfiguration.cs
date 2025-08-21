using Leadership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leadership.Infrastructure.Persistance.Configurations
{
    public class LeaderboardEntryConfiguration : IEntityTypeConfiguration<LeaderboardEntry>
    {
        public void Configure(EntityTypeBuilder<LeaderboardEntry> builder)
        {
            builder.ToTable("LeaderboardEntries");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.PlayerId).IsRequired();
            builder.Property(x => x.IsActive).HasDefaultValue(true);


            builder.OwnsOne(x => x.Rank, rank =>
            {
                rank.Property(p => p.RankPoints).HasColumnName("RankPoints").IsRequired();
                rank.Property(p => p.Position).HasColumnName("Position").IsRequired();
                rank.HasIndex(p => p.RankPoints);
            });


            builder.HasIndex(x => new { x.LeaderboardId, x.PlayerId }).IsUnique();
        }
    }
}
