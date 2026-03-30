using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        }
    }
}
