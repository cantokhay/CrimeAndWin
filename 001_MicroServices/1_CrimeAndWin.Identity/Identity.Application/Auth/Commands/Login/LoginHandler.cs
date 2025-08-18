using Identity.Application.Auth.Abstract;
using Identity.Application.Auth.DTOs;
using Identity.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Shared.Domain.Repository;

namespace Identity.Application.Auth.Commands.Login
{
    public sealed class LoginHandler : IRequestHandler<LoginCommand, AuthResultDTO>
    {
        private readonly IReadRepository<AppUser> _usersRead;
        private readonly IReadRepository<UserRole> _userRolesRead;
        private readonly IReadRepository<Role> _rolesRead;
        private readonly IReadRepository<UserClaim> _userClaimsRead;
        private readonly IReadRepository<RoleClaim> _roleClaimsRead;
        private readonly IWriteRepository<RefreshToken> _refreshWrite;
        private readonly IJwtTokenGenerator _jwt;

        public LoginHandler(
            IReadRepository<AppUser> usersRead,
            IReadRepository<UserRole> userRolesRead,
            IReadRepository<Role> rolesRead,
            IReadRepository<UserClaim> userClaimsRead,
            IReadRepository<RoleClaim> roleClaimsRead,
            IWriteRepository<RefreshToken> refreshWrite,
            IJwtTokenGenerator jwt)
        {
            _usersRead = usersRead;
            _userRolesRead = userRolesRead;
            _rolesRead = rolesRead;
            _userClaimsRead = userClaimsRead;
            _roleClaimsRead = roleClaimsRead;
            _refreshWrite = refreshWrite;
            _jwt = jwt;
        }

        public async Task<AuthResultDTO> Handle(LoginCommand request, CancellationToken ct)
        {
            // 1) Kullanıcıyı bul
            var norm = request.UserNameOrEmail.Trim().ToUpperInvariant();

            var user = await _usersRead
                .GetWhere(u => u.NormalizedUserName == norm || u.NormalizedEmail == norm, tracking: true)
                .FirstOrDefaultAsync(ct);

            if (user is null)
                throw new UnauthorizedAccessException("Kullanıcı adı/e-posta veya şifre hatalı.");

            // 2) Şifre doğrulama
            var hasher = new PasswordHasher<AppUser>();
            var verify = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (verify == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Kullanıcı adı/e-posta veya şifre hatalı.");

            // 3) Roller
            var roleIds = await _userRolesRead
                .GetWhere(ur => ur.UserId == user.Id, tracking: false)
                .Select(ur => ur.RoleId)
                .ToListAsync(ct);

            var roles = await _rolesRead
                .GetWhere(r => roleIds.Contains(r.Id), tracking: false)
                .Select(r => r.Name)
                .ToListAsync(ct);

            // 4) Claims (user + role)
            var userClaims = await _userClaimsRead
                .GetWhere(c => c.UserId == user.Id, tracking: false)
                .Select(c => new KeyValuePair<string, string>(c.ClaimType, c.ClaimValue))
                .ToListAsync(ct);

            var roleClaimPairs = await _roleClaimsRead
                .GetWhere(rc => roleIds.Contains(rc.RoleId), tracking: false)
                .Select(rc => new KeyValuePair<string, string>(rc.ClaimType, rc.ClaimValue))
                .ToListAsync(ct);

            var allClaims = userClaims.Concat(roleClaimPairs).ToList();

            // 5) Token üret
            var (accessToken, expiresAtUtc) = _jwt.Generate(user, roles, allClaims);
            var refreshTokenValue = _jwt.GenerateRefreshToken();

            // 6) RefreshToken kaydet
            var refresh = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = refreshTokenValue,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(7), // JwtOptions.RefreshTokenDays kullanılabilir
                CreatedAtUtc = DateTime.UtcNow,
                IsDeleted = false
            };

            await _refreshWrite.AddAsync(refresh);
            await _refreshWrite.SaveAsync();

            return new AuthResultDTO
            {
                AccessToken = accessToken,
                ExpiresAtUtc = expiresAtUtc,
                RefreshToken = refreshTokenValue,
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
