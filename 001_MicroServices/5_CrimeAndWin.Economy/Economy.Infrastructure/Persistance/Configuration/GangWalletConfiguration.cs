using Economy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Economy.Infrastructure.Persistance.Configuration
{
    public class GangWalletConfiguration : IEntityTypeConfiguration<GangWallet>
    {
        public void Configure(EntityTypeBuilder<GangWallet> b)
        {
            b.ToTable("GangWallets");
            b.HasKey(x => x.Id);

            b.Property(x => x.GangId).IsRequired();
            b.Property(x => x.BlackBalance).HasColumnName("BlackMoney").HasPrecision(18, 2).HasDefaultValue(0);
            b.Property(x => x.CashBalance).HasColumnName("CashBalance").HasPrecision(18, 2).HasDefaultValue(0);
            b.Property(x => x.MaxCapacity).HasPrecision(18, 2).HasDefaultValue(1000000);
            b.Property(x => x.Note).HasMaxLength(500);

            // Seed GangWallets
            b.HasData(
                new GangWallet 
                { 
                    Id = SeedDataConstants.GangWalletBloodlineId, 
                    GangId = SeedDataConstants.GangBloodlineId, 
                    BlackBalance = 1500000.00m, 
                    CashBalance = 3500000.00m,
                    MaxCapacity = 5000000.00m,
                    Note = "Initial test vault",
                    LastTransactionAt = SeedDataConstants.SeedDate,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new GangWallet 
                { 
                    Id = SeedDataConstants.GangWalletSiliconId, 
                    GangId = SeedDataConstants.GangSiliconId, 
                    BlackBalance = 350000.00m, 
                    CashBalance = 2150000.00m,
                    MaxCapacity = 3000000.00m,
                    Note = "Secondary test vault",
                    LastTransactionAt = SeedDataConstants.SeedDate,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}
