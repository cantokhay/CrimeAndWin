using Economy.Domain.Entities;
using Economy.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

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
                
                // Seed data mapping for Owned Types
                m.HasData(
                    new { TransactionId = SeedDataConstants.TransactionAlpha1Id, Amount = 5000.00m, CurrencyType = "USD" },
                    new { TransactionId = SeedDataConstants.TransactionAlpha2Id, Amount = 2000.00m, CurrencyType = "USD" },
                    new { TransactionId = SeedDataConstants.TransactionBeta1Id, Amount = 1500.00m, CurrencyType = "USD" }
                );
            });

            builder.OwnsOne(x => x.Reason, r =>
            {
                r.Property(p => p.ReasonCode).HasColumnName("ReasonCode").HasMaxLength(50).IsRequired();
                r.Property(p => p.Description).HasColumnName("Description").HasMaxLength(200);

                // Seed data mapping for Owned Types
                r.HasData(
                    new { TransactionId = SeedDataConstants.TransactionAlpha1Id, ReasonCode = "CRIME_REWARD", Description = "Reward from Store Robbery" },
                    new { TransactionId = SeedDataConstants.TransactionAlpha2Id, ReasonCode = "MONEY_WASH", Description = "Cleaned money" },
                    new { TransactionId = SeedDataConstants.TransactionBeta1Id, ReasonCode = "CRIME_REWARD", Description = "Reward from Pickpocket" }
                );
            });

            builder.Property(x => x.BalanceType).IsRequired();

            // Seed root entity
            builder.HasData(
                new Transaction
                {
                    Id = SeedDataConstants.TransactionAlpha1Id,
                    WalletId = SeedDataConstants.WalletAlphaId,
                    BalanceType = WalletBalanceType.BlackMoney,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Transaction
                {
                    Id = SeedDataConstants.TransactionAlpha2Id,
                    WalletId = SeedDataConstants.WalletAlphaId,
                    BalanceType = WalletBalanceType.CashBalance,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Transaction
                {
                    Id = SeedDataConstants.TransactionBeta1Id,
                    WalletId = SeedDataConstants.WalletBetaId,
                    BalanceType = WalletBalanceType.BlackMoney,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}


