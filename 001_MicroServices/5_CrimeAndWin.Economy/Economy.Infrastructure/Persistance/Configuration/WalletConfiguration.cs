using Economy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Economy.Infrastructure.Persistance.Configuration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> b)
        {
            b.ToTable("Wallets");
            b.HasKey(x => x.Id);

            b.Property(x => x.PlayerId).IsRequired();
            b.Property(x => x.Balance).HasPrecision(18, 2).HasDefaultValue(0);
            b.Property(x => x.BlackBalance).HasColumnName("BlackMoney").HasPrecision(18, 2).HasDefaultValue(0);
            b.Property(x => x.CashBalance).HasColumnName("CashBalance").HasPrecision(18, 2).HasDefaultValue(0);

            b.HasMany(x => x.Transactions)
             .WithOne(x => x.Wallet)
             .HasForeignKey(x => x.WalletId);

            // Seed Wallets
            b.HasData(
                new Wallet 
                { 
                    Id = SeedDataConstants.WalletAlphaId, 
                    PlayerId = SeedDataConstants.PlayerAlphaId, 
                    Balance = 1250000.00m, 
                    BlackBalance = 75000.00m, 
                    CashBalance = 1175000.00m,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new Wallet 
                { 
                    Id = SeedDataConstants.WalletBetaId, 
                    PlayerId = SeedDataConstants.PlayerBetaId, 
                    Balance = 45000.00m, 
                    BlackBalance = 185000.00m, 
                    CashBalance = 45000.00m,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}
