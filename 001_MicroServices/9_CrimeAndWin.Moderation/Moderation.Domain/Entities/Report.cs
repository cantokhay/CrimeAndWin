using Moderation.Domain.VOs;
using Shared.Domain;

namespace Moderation.Domain.Entities
{
    public class Report : BaseEntity
    {
        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }
        public ReportReason Reason { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAtUtc { get; set; }
        public Guid? ResolvedByModeratorId { get; set; }
    }
}
