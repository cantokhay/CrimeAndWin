namespace Moderation.Application.DTOs.ReportDTOs.Admin
{
    public sealed class AdminResultReportDTO
    {
        public Guid Id { get; set; }

        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }

        public string Reason { get; set; } = null!;
        public string Description { get; set; } = null!;

        public bool IsResolved { get; set; }
        public DateTime? ResolvedAtUtc { get; set; }
        public Guid? ResolvedByModeratorId { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
