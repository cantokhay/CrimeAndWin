using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moderation.Domain.Entities;

namespace Moderation.Infrastructure.Persistance.Configurations
{
    public class ModerationActionConfiguration : IEntityTypeConfiguration<ModerationAction>
    {
        public void Configure(EntityTypeBuilder<ModerationAction> b)
        {
            b.ToTable("ModerationActions");
            b.HasKey(x => x.Id);

            b.Property(x => x.PlayerId).IsRequired();
            b.Property(x => x.ModeratorId).IsRequired();

            b.Property(x => x.ActionType).HasMaxLength(30).IsRequired();
            b.Property(x => x.Reason).HasMaxLength(500).IsRequired();

            b.Property(x => x.ActionDateUtc).IsRequired();
            b.Property(x => x.ExpiryDateUtc);
            b.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
