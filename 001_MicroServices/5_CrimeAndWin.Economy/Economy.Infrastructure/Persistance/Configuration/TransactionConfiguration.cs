using Economy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Economy.Infrastructure.Persistance.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Wallet)
                   .WithMany(w => w.Transactions)
                   .HasForeignKey(t => t.WalletId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(x => x.Money, m =>
            {
                m.Property(p => p.Amount).HasColumnName("Amount").HasPrecision(18, 2).IsRequired();
                m.Property(p => p.CurrencyType).HasColumnName("CurrencyType").HasMaxLength(5).IsRequired();
            });

            builder.OwnsOne(x => x.Reason, r =>
            {
                r.Property(p => p.ReasonCode).HasColumnName("ReasonCode").HasMaxLength(50).IsRequired();
                r.Property(p => p.Description).HasColumnName("Description").HasMaxLength(200);
            });
        }
    }
}
