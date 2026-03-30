using Shared.Domain;

namespace Identity.Domain.Entities
{
    public class UserClaim : BaseEntity
    {
        public Guid UserId { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;

        public AppUser User { get; set; } = null!;
    }
}
