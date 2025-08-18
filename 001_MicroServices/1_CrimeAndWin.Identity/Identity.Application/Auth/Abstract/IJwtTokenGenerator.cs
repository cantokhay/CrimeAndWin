using Identity.Domain.Entities;

namespace Identity.Application.Auth.Abstract
{
    public interface IJwtTokenGenerator
    {
        (string accessToken, DateTime expiresAtUtc) Generate(AppUser user, IEnumerable<string> roles, IEnumerable<KeyValuePair<string, string>> claims);
        string GenerateRefreshToken(); // basit random; ileride rotate/blacklist ekleriz
    }
}
