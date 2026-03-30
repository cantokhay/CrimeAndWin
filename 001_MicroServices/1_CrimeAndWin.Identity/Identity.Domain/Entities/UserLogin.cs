using Shared.Domain;

namespace Identity.Domain.Entities
{
    public class UserLogin : BaseEntity
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }

        public AppUser User { get; set; } = null!;
    }
}
