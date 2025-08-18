using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configuration
{
    public class OtherConfiguration
    {
        public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
        {
            public void Configure(EntityTypeBuilder<UserRole> b)
            {
                b.ToTable("UserRoles");
                b.HasKey(x => x.Id);
                b.HasIndex(x => new { x.UserId, x.RoleId }).IsUnique();
            }
        }

        public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
        {
            public void Configure(EntityTypeBuilder<UserClaim> b)
            {
                b.ToTable("UserClaims");
                b.HasKey(x => x.Id);
                b.Property(x => x.ClaimType).HasMaxLength(128).IsRequired();
                b.Property(x => x.ClaimValue).HasMaxLength(512).IsRequired();
            }
        }

        public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
        {
            public void Configure(EntityTypeBuilder<RoleClaim> b)
            {
                b.ToTable("RoleClaims");
                b.HasKey(x => x.Id);
                b.Property(x => x.ClaimType).HasMaxLength(128).IsRequired();
                b.Property(x => x.ClaimValue).HasMaxLength(512).IsRequired();
            }
        }

        public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
        {
            public void Configure(EntityTypeBuilder<UserLogin> b)
            {
                b.ToTable("UserLogins");
                b.HasKey(x => x.Id);
                b.HasIndex(x => new { x.LoginProvider, x.ProviderKey }).IsUnique();
                b.Property(x => x.LoginProvider).HasMaxLength(64).IsRequired();
                b.Property(x => x.ProviderKey).HasMaxLength(256).IsRequired();
                b.Property(x => x.ProviderDisplayName).HasMaxLength(128);
            }
        }

        public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
        {
            public void Configure(EntityTypeBuilder<UserToken> b)
            {
                b.ToTable("UserTokens");
                b.HasKey(x => x.Id);
                b.HasIndex(x => new { x.UserId, x.LoginProvider, x.Name }).IsUnique();
                b.Property(x => x.LoginProvider).HasMaxLength(64).IsRequired();
                b.Property(x => x.Name).HasMaxLength(128).IsRequired();
            }
        }

        public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
        {
            public void Configure(EntityTypeBuilder<RefreshToken> b)
            {
                b.ToTable("RefreshTokens");
                b.HasKey(x => x.Id);
                b.HasIndex(x => x.Token).IsUnique();
                b.Property(x => x.Token).HasMaxLength(256).IsRequired();
                b.Property(x => x.ExpiresAtUtc).IsRequired();
            }
        }
    }
}
