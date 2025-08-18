using Shared.Domain;

namespace Identity.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public AppUser User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}
