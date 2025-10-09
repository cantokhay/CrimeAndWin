using Shared.Domain;

namespace Moderation.Domain.Entities
{
    public class ModerationAction : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public string ActionType { get; set; }     // Ban, Restrict, Warning
        public string Reason { get; set; }         // Serbest metin
        public DateTime ActionDateUtc { get; set; }
        public DateTime? ExpiryDateUtc { get; set; } // Süreli ban/restrict
        public Guid ModeratorId { get; set; }
        public bool IsActive { get; set; }         // unban/ kaldırma ile false
    }
}
