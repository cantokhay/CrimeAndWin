using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moderation.Domain.Entities;
using Moderation.Domain.VOs;

namespace Moderation.Infrastructure.Persistance.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> b)
        {
            b.ToTable("Reports");
            b.HasKey(x => x.Id);

            b.Property(x => x.ReporterId).IsRequired();
            b.Property(x => x.ReportedPlayerId).IsRequired();

            // ValueObject -> string conversion
            b.Property(x => x.Reason)
             .HasConversion(
                to => to.Value,
                from => new ReportReason(from))
             .HasMaxLength(50)
             .IsRequired();

            b.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            b.Property(x => x.IsResolved).HasDefaultValue(false);
            b.Property(x => x.ResolvedAtUtc);
            b.Property(x => x.ResolvedByModeratorId);
        }
    }
}
