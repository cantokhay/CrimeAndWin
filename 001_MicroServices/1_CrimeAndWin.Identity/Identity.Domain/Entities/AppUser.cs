using Shared.Domain;

namespace Identity.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; } = null!;
        public string NormalizedUserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NormalizedEmail { get; set; } = null!;
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; } = null!;
        public string SecurityStamp { get; set; } = null!;
        public string ConcurrencyStamp { get; set; } = null!;

        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserClaim> Claims { get; set; } = new List<UserClaim>();
        public ICollection<UserLogin> Logins { get; set; } = new List<UserLogin>();
        public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
