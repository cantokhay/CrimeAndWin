using Leadership.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Leadership.Infrastructure.Persistance.Configurations
{
    public class LeaderboardConfiguration : IEntityTypeConfiguration<Leaderboard>
    {
        public void Configure(EntityTypeBuilder<Leaderboard> builder)
        {
            builder.ToTable("Leaderboards");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);


            builder.Property(x => x.Description)
            .HasMaxLength(500);

            builder.HasMany(x => x.Entries)
            .WithOne(l => l.Leaderboard)
            .HasForeignKey(e => e.LeaderboardId)
            .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(x => new { x.GameWorldId, x.SeasonId });
        }
    }
}
