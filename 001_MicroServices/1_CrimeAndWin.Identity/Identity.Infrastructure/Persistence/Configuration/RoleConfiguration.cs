using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CrimeAndWin.Shared.Constants;

namespace Identity.Infrastructure.Persistence.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> b)
        {
            b.ToTable("Roles");
            b.HasKey(x => x.Id);

            b.Property(x => x.Name).HasMaxLength(100).IsRequired();
            b.Property(x => x.NormalizedName).HasMaxLength(100).IsRequired();
            b.HasIndex(x => x.NormalizedName).IsUnique();

            b.HasMany(x => x.UserRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            b.HasMany(x => x.Claims).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);

            // Seed Roles
            b.HasData(
                new Role { Id = SeedDataConstants.AdminRoleId, Name = "Admin", NormalizedName = "ADMIN", Description = "System Administrator", CreatedAtUtc = SeedDataConstants.SeedDate, IsDeleted = false },
                new Role { Id = SeedDataConstants.PlayerRoleId, Name = "Player", NormalizedName = "PLAYER", Description = "Standard Player", CreatedAtUtc = SeedDataConstants.SeedDate, IsDeleted = false },
                new Role { Id = SeedDataConstants.ModeratorRoleId, Name = "Moderator", NormalizedName = "MODERATOR", Description = "Community Moderator", CreatedAtUtc = SeedDataConstants.SeedDate, IsDeleted = false }
            );
        }
    }
}
