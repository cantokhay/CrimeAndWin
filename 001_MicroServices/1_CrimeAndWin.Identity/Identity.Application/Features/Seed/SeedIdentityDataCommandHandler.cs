using Bogus;
using MediatR;
using Shared.Domain.Repository;
using Shared.Domain.Time;

namespace Identity.Application.Features.Seed
{
    public sealed class SeedIdentityDataCommandHandler : IRequestHandler<SeedIdentityDataCommand, string>
    {
        private readonly IWriteRepository<Domain.Entities.AppUser> _userWrite;
        private readonly IWriteRepository<Domain.Entities.Role> _roleWrite;
        private readonly IWriteRepository<Domain.Entities.UserRole> _userRoleWrite;
        private readonly IWriteRepository<Domain.Entities.UserClaim> _userClaimWrite;
        private readonly IWriteRepository<Domain.Entities.UserLogin> _userLoginWrite;
        private readonly IWriteRepository<Domain.Entities.UserToken> _userTokenWrite;
        private readonly IWriteRepository<Domain.Entities.RefreshToken> _refreshTokenWrite;
        private readonly IDateTimeProvider _time;

        public SeedIdentityDataCommandHandler(
            IWriteRepository<Domain.Entities.AppUser> userWrite,
            IWriteRepository<Domain.Entities.Role> roleWrite,
            IWriteRepository<Domain.Entities.UserRole> userRoleWrite,
            IWriteRepository<Domain.Entities.UserClaim> userClaimWrite,
            IWriteRepository<Domain.Entities.UserLogin> userLoginWrite,
            IWriteRepository<Domain.Entities.UserToken> userTokenWrite,
            IWriteRepository<Domain.Entities.RefreshToken> refreshTokenWrite,
            IDateTimeProvider time)
        {
            _userWrite = userWrite;
            _roleWrite = roleWrite;
            _userRoleWrite = userRoleWrite;
            _userClaimWrite = userClaimWrite;
            _userLoginWrite = userLoginWrite;
            _userTokenWrite = userTokenWrite;
            _refreshTokenWrite = refreshTokenWrite;
            _time = time;
        }

        public async Task<string> Handle(SeedIdentityDataCommand request, CancellationToken cancellationToken)
        {
            var now = _time.UtcNow;
            var count = 10;

            // 🧩 Users
            var users = new Faker<Domain.Entities.AppUser>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.UserName, f => f.Internet.UserName())
                .RuleFor(x => x.NormalizedUserName, (f, u) => u.UserName.ToUpperInvariant())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.NormalizedEmail, (f, u) => u.Email.ToUpperInvariant())
                .RuleFor(x => x.EmailConfirmed, f => f.Random.Bool())
                .RuleFor(x => x.PasswordHash, f => f.Internet.Password())
                .RuleFor(x => x.SecurityStamp, _ => Guid.NewGuid().ToString())
                .RuleFor(x => x.ConcurrencyStamp, _ => Guid.NewGuid().ToString())
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.PhoneNumberConfirmed, f => f.Random.Bool())
                .RuleFor(x => x.TwoFactorEnabled, f => f.Random.Bool())
                .RuleFor(x => x.LockoutEnabled, f => f.Random.Bool())
                .RuleFor(x => x.AccessFailedCount, f => f.Random.Int(0, 5))
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _userWrite.AddRangeAsync(users);
            await _userWrite.SaveAsync();

            // 🧩 Roles
            var roles = new Faker<Domain.Entities.Role>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.PickRandom(new[] { "Player", "Moderator", "Admin", "VIP", "Support", "Guest" }))
                .RuleFor(x => x.NormalizedName, (f, r) => r.Name.ToUpperInvariant() + f.Random.Int(0,50).ToString())
                .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _roleWrite.AddRangeAsync(roles);
            await _roleWrite.SaveAsync();

            // 🧩 UserRoles
            var userRoles = users.Select((u, i) => new Domain.Entities.UserRole
            {
                Id = Guid.NewGuid(),
                UserId = u.Id,
                RoleId = roles[i % roles.Count].Id,
                CreatedAtUtc = now
            }).ToList();

            await _userRoleWrite.AddRangeAsync(userRoles);
            await _userRoleWrite.SaveAsync();

            // 🧩 UserClaims
            var userClaims = new Faker<Domain.Entities.UserClaim>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.UserId, f => f.PickRandom(users).Id)
                .RuleFor(x => x.ClaimType, f => f.PickRandom("Permission", "Rank", "Status"))
                .RuleFor(x => x.ClaimValue, f => f.PickRandom("Basic", "Gold", "Platinum"))
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _userClaimWrite.AddRangeAsync(userClaims);
            await _userClaimWrite.SaveAsync();

            // 🧩 UserLogins
            var userLogins = new Faker<Domain.Entities.UserLogin>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.UserId, f => f.PickRandom(users).Id)
                .RuleFor(x => x.LoginProvider, f => f.PickRandom("Google", "Facebook", "Discord"))
                .RuleFor(x => x.ProviderKey, f => f.Random.Guid().ToString())
                .RuleFor(x => x.ProviderDisplayName, f => f.Company.CompanyName())
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _userLoginWrite.AddRangeAsync(userLogins);
            await _userLoginWrite.SaveAsync();

            // 🧩 UserTokens
            var userTokens = new Faker<Domain.Entities.UserToken>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.UserId, f => f.PickRandom(users).Id)
                .RuleFor(x => x.LoginProvider, f => f.PickRandom("App", "Web", "Mobile"))
                .RuleFor(x => x.Name, f => f.PickRandom("AccessToken", "EmailVerify", "PasswordReset"))
                .RuleFor(x => x.Value, f => f.Random.AlphaNumeric(20))
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _userTokenWrite.AddRangeAsync(userTokens);
            await _userTokenWrite.SaveAsync();

            // 🧩 RefreshTokens
            var refreshTokens = new Faker<Domain.Entities.RefreshToken>("tr")
                .RuleFor(x => x.Id, _ => Guid.NewGuid())
                .RuleFor(x => x.UserId, f => f.PickRandom(users).Id)
                .RuleFor(x => x.Token, f => f.Random.AlphaNumeric(40))
                .RuleFor(x => x.ExpiresAtUtc, f => now.AddDays(30))
                .RuleFor(x => x.RevokedAtUtc, _ => null)
                .RuleFor(x => x.ReplacedByToken, _ => null)
                .RuleFor(x => x.CreatedAtUtc, _ => now)
                .Generate(count);

            await _refreshTokenWrite.AddRangeAsync(refreshTokens);
            await _refreshTokenWrite.SaveAsync();

            return "Repository üzerinden Identity seed işlemi başarıyla tamamlandı (her entity 10 kayıt).";
        }
    }
}
