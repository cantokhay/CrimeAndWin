namespace Moderation.Application.DTOs.ReportDTOs
{
    public class ResultReportDTO
    {
        public Guid Id { get; set; }
        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? ResolvedAtUtc { get; set; }
        public Guid? ResolvedByModeratorId { get; set; }
    }
}
