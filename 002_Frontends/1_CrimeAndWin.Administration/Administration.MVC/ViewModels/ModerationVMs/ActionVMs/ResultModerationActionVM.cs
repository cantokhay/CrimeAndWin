namespace Administration.MVC.ViewModels.ModerationVMs.ActionVMs
{
    public class ResultModerationActionVM
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }
        public string ActionType { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public DateTime ActionDateUtc { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public Guid ModeratorId { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }

        public string? PlayerDisplay { get; set; }
    }
}
