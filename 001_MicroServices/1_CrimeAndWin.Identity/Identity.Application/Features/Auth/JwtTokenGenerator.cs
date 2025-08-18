using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Application.Features.Auth.Abstract;
using Identity.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Application.Features.Auth
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _opt;
        public JwtTokenGenerator(IOptions<JwtOptions> opt) => _opt = opt.Value;

        public (string accessToken, DateTime expiresAtUtc) Generate(AppUser user, IEnumerable<string> roles, IEnumerable<KeyValuePair<string, string>> claims)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_opt.AccessTokenMinutes);

            var claimsList = new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new(JwtRegisteredClaimNames.Email, user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
                };

            foreach (var role in roles) claimsList.Add(new Claim(ClaimTypes.Role, role));
            foreach (var kv in claims) claimsList.Add(new Claim(kv.Key, kv.Value));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _opt.Issuer,
                audience: _opt.Audience,
                claims: claimsList,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return (jwt, expires);
        }

        public string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
