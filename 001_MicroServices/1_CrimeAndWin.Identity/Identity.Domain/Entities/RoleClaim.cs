using Shared.Domain;

namespace Identity.Domain.Entities
{

    public class RoleClaim : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; } = null!;
        public string ClaimValue { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}
