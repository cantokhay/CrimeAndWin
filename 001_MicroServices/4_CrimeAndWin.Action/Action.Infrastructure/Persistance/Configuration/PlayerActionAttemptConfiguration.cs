using Action.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Action.Infrastructure.Persistance.Configuration
{
    public class PlayerActionAttemptConfiguration : IEntityTypeConfiguration<PlayerActionAttempt>
    {
        public void Configure(EntityTypeBuilder<PlayerActionAttempt> b)
        {
            b.ToTable("PlayerActionAttempts");
            b.HasKey(x => x.Id);

            b.Property(x => x.PlayerId).IsRequired();
            b.Property(x => x.ActionDefinitionId).IsRequired();
            b.Property(x => x.AttemptedAtUtc).IsRequired();

            b.OwnsOne(x => x.PlayerActionResults, nb =>
            {
                nb.Property(p => p.SuccessRate).HasColumnName("SuccessRate").HasPrecision(5, 2);
                nb.Property(p => p.OutcomeType).HasColumnName("OutcomeType");
            });

            b.HasIndex(x => new { x.PlayerId, x.ActionDefinitionId, x.AttemptedAtUtc });
        }
    }
}
