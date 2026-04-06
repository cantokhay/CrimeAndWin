using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Identity.Infrastructure.Persistence.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> b)
        {
            b.ToTable("Users");
            b.HasKey(x => x.Id);

            b.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            b.Property(x => x.NormalizedUserName).HasMaxLength(100).IsRequired();
            b.Property(x => x.Email).HasMaxLength(256).IsRequired();
            b.Property(x => x.NormalizedEmail).HasMaxLength(256).IsRequired();
            b.HasIndex(x => x.NormalizedUserName).IsUnique();
            b.HasIndex(x => x.NormalizedEmail);

            b.Property(x => x.PasswordHash).IsRequired();
            b.Property(x => x.SecurityStamp).HasMaxLength(64).IsRequired();
            b.Property(x => x.ConcurrencyStamp).HasMaxLength(64).IsRequired();

            b.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            b.HasMany(x => x.Claims).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            b.HasMany(x => x.Logins).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            b.HasMany(x => x.Tokens).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            b.HasMany(x => x.RefreshTokens).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            // Seed Users (Pass: Admin123*)
            b.HasData(
                new AppUser 
                { 
                    Id = SeedDataConstants.AdminUserId, 
                    UserName = "admin", 
                    NormalizedUserName = "ADMIN", 
                    Email = "admin@crimeandwin.com", 
                    NormalizedEmail = "ADMIN@CRIMEANDWIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEI9h+Lp...HashPlaceholder", 
                    SecurityStamp = "stamp-admin",
                    ConcurrencyStamp = "concurrency-admin",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsApproved = true,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new AppUser 
                { 
                    Id = SeedDataConstants.UserAlphaId, 
                    UserName = "Alpha", 
                    NormalizedUserName = "ALPHA", 
                    Email = "alpha@crimeandwin.com", 
                    NormalizedEmail = "ALPHA@CRIMEANDWIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEI9h+Lp...HashPlaceholder", 
                    SecurityStamp = "stamp-alpha",
                    ConcurrencyStamp = "concurrency-alpha",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsApproved = true,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                },
                new AppUser 
                { 
                    Id = SeedDataConstants.UserBetaId, 
                    UserName = "Beta", 
                    NormalizedUserName = "BETA", 
                    Email = "beta@crimeandwin.com", 
                    NormalizedEmail = "BETA@CRIMEANDWIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEI9h+Lp...HashPlaceholder", 
                    SecurityStamp = "stamp-beta",
                    ConcurrencyStamp = "concurrency-beta",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    IsApproved = true,
                    CreatedAtUtc = SeedDataConstants.SeedDate,
                    IsDeleted = false
                }
            );
        }
    }
}
