namespace Administration.MVC.ViewModels.ModerationVMs.ReportVMs
{
    public class ResultReportVM
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

        public string? ReporterDisplay { get; set; }
        public string? ReportedPlayerDisplay { get; set; }
    }
}
