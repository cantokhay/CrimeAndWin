using Bogus;
using Shared.Application.Abstractions.Messaging;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.Seed
{
    public sealed class SeedIdentityDataCommandHandler : IRequestHandler<SeedIdentityDataCommand, string>
    {
        private readonly IWriteRepository<Domain.Entities.AppUser> _userWrite;
        private readonly IWriteRepository<Domain.Entities.Role> _roleWrite;
        private readonly IWriteRepository<Domain.Entities.UserRole> _userRoleWrite;
        private readonly IDateTimeProvider _time;

        public SeedIdentityDataCommandHandler(
            IWriteRepository<Domain.Entities.AppUser> userWrite,
            IWriteRepository<Domain.Entities.Role> roleWrite,
            IWriteRepository<Domain.Entities.UserRole> userRoleWrite,
            IDateTimeProvider time)
        {
            _userWrite = userWrite;
            _roleWrite = roleWrite;
            _userRoleWrite = userRoleWrite;
            _time = time;
        }

        public async Task<string> Handle(SeedIdentityDataCommand request, CancellationToken ct)
        {
            var now = _time.UtcNow;
            var users = new List<Domain.Entities.AppUser>();
            
            // Create specific "Theme" users
            var themeUsers = new[] { "Boss", "Hitman", "Mole", "Fixer", "Dealer", "Enforcer", "Launderer" };
            foreach (var name in themeUsers)
            {
                users.Add(new Domain.Entities.AppUser
                {
                    Id = Guid.Parse($"00000000-0000-0000-0000-{users.Count:D12}"), // Deterministic
                    UserName = name.ToLower(),
                    NormalizedUserName = name.ToUpper(),
                    Email = $"{name.ToLower()}@crimeandwin.com",
                    NormalizedEmail = $"{name.ToUpper()}@CRIMEANDWIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAE...", 
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    CreatedAtUtc = now
                });
            }

            // Roles
            var roles = new[] { "Godfather", "Underboss", "Consigliere", "Capo", "Soldier", "Associate" }
                .Select((r, i) => new Domain.Entities.Role 
                { 
                    Id = Guid.Parse($"11111111-1111-1111-1111-{i:D12}"),
                    Name = r, 
                    NormalizedName = r.ToUpper(),
                    Description = $"{r} rank in the organization.",
                    CreatedAtUtc = now
                }).ToList();

            try {
                await _roleWrite.AddRangeAsync(roles);
                await _userWrite.AddRangeAsync(users);
                await _roleWrite.SaveAsync();
                await _userWrite.SaveAsync();

                var userRoles = users.Select((u, i) => new Domain.Entities.UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = u.Id,
                    RoleId = roles[i % roles.Count].Id,
                    CreatedAtUtc = now
                }).ToList();
                await _userRoleWrite.AddRangeAsync(userRoles);
                await _userRoleWrite.SaveAsync();

            } catch { }

            return "Identity seeded with thematic ranks and users.";
        }
    }
}
