using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;
using Shared.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Features.Seed
{
    public sealed class SeedIdentityDataCommandHandler : IRequestHandler<SeedIdentityDataCommand, string>
    {
        private readonly IWriteRepository<global::Identity.Domain.Entities.AppUser> _userWrite;
        private readonly IReadRepository<global::Identity.Domain.Entities.AppUser> _userRead;
        private readonly IWriteRepository<global::Identity.Domain.Entities.Role> _roleWrite;
        private readonly IReadRepository<global::Identity.Domain.Entities.Role> _roleRead;
        private readonly IWriteRepository<global::Identity.Domain.Entities.UserRole> _userRoleWrite;
        private readonly IReadRepository<global::Identity.Domain.Entities.UserRole> _userRoleRead;
        private readonly IDateTimeProvider _time;

        public SeedIdentityDataCommandHandler(
            IWriteRepository<global::Identity.Domain.Entities.AppUser> userWrite,
            IReadRepository<global::Identity.Domain.Entities.AppUser> userRead,
            IWriteRepository<global::Identity.Domain.Entities.Role> roleWrite,
            IReadRepository<global::Identity.Domain.Entities.Role> roleRead,
            IWriteRepository<global::Identity.Domain.Entities.UserRole> userRoleWrite,
            IReadRepository<global::Identity.Domain.Entities.UserRole> userRoleRead,
            IDateTimeProvider time)
        {
            _userWrite = userWrite;
            _userRead = userRead;
            _roleWrite = roleWrite;
            _roleRead = roleRead;
            _userRoleWrite = userRoleWrite;
            _userRoleRead = userRoleRead;
            _time = time;
        }

        public async Task<string> Handle(SeedIdentityDataCommand request, CancellationToken ct)
        {
            var seedDate = SeedDataConstants.SeedDate;

            // 1. ROLES
            var rolesToSeed = new List<global::Identity.Domain.Entities.Role>
            {
                new() { Id = SeedDataConstants.AdminRoleId, Name = "Admin", NormalizedName = "ADMIN", CreatedAtUtc = seedDate },
                new() { Id = SeedDataConstants.PlayerRoleId, Name = "Player", NormalizedName = "PLAYER", CreatedAtUtc = seedDate },
                new() { Id = SeedDataConstants.ModeratorRoleId, Name = "Moderator", NormalizedName = "MODERATOR", CreatedAtUtc = seedDate }
            };

            foreach (var role in rolesToSeed)
            {
                if (await _roleRead.GetByIdAsync(role.Id.ToString()) == null)
                {
                    await _roleWrite.AddAsync(role);
                }
            }
            await _roleWrite.SaveAsync();

            // 2. USERS
            var usersToSeed = new List<global::Identity.Domain.Entities.AppUser>
            {
                new() 
                { 
                    Id = SeedDataConstants.AdminUserId, UserName = "admin", NormalizedUserName = "ADMIN", 
                    Email = "admin@crimeandwin.com", NormalizedEmail = "ADMIN@CRIMEANDWIN.COM", 
                    EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAE...", 
                    SecurityStamp = Guid.NewGuid().ToString(), CreatedAtUtc = seedDate, IsApproved = true 
                },
                new() 
                { 
                    Id = SeedDataConstants.UserAlphaId, UserName = "alpha", NormalizedUserName = "ALPHA", 
                    Email = "alpha@crimeandwin.com", NormalizedEmail = "ALPHA@CRIMEANDWIN.COM", 
                    EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAE...", 
                    SecurityStamp = Guid.NewGuid().ToString(), CreatedAtUtc = seedDate, IsApproved = true 
                },
                new() 
                { 
                    Id = SeedDataConstants.UserBetaId, UserName = "beta", NormalizedUserName = "BETA", 
                    Email = "beta@crimeandwin.com", NormalizedEmail = "BETA@CRIMEANDWIN.COM", 
                    EmailConfirmed = true, PasswordHash = "AQAAAAIAAYagAAAAE...", 
                    SecurityStamp = Guid.NewGuid().ToString(), CreatedAtUtc = seedDate, IsApproved = true 
                }
            };

            foreach (var user in usersToSeed)
            {
                if (await _userRead.GetByIdAsync(user.Id.ToString()) == null)
                {
                    await _userWrite.AddAsync(user);
                }
            }
            await _userWrite.SaveAsync();

            // 3. USER ROLES
            var userRolesToSeed = new List<global::Identity.Domain.Entities.UserRole>
            {
                new() { Id = SeedDataConstants.UserRoleAdminId, UserId = SeedDataConstants.AdminUserId, RoleId = SeedDataConstants.AdminRoleId, CreatedAtUtc = seedDate },
                new() { Id = SeedDataConstants.UserRoleAlphaId, UserId = SeedDataConstants.UserAlphaId, RoleId = SeedDataConstants.PlayerRoleId, CreatedAtUtc = seedDate },
                new() { Id = SeedDataConstants.UserRoleBetaId, UserId = SeedDataConstants.UserBetaId, RoleId = SeedDataConstants.PlayerRoleId, CreatedAtUtc = seedDate }
            };

            foreach (var ur in userRolesToSeed)
            {
                if (await _userRoleRead.GetByIdAsync(ur.Id.ToString()) == null)
                {
                    await _userRoleWrite.AddAsync(ur);
                }
            }
            await _userRoleWrite.SaveAsync();

            return "Identity seed completed successfully with deterministic GUIDs.";
        }
    }
}
