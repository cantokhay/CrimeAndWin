using Shared.Domain;

namespace Moderation.Domain.Entities
{
    public class ModerationAction : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public string ActionType { get; set; }
        public string Reason { get; set; }
        public DateTime ActionDateUtc { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public Guid ModeratorId { get; set; }
        public bool IsActive { get; set; }
    }
}
