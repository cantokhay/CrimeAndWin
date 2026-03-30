namespace Moderation.Application.DTOs.ModerationActionDTOs.Admin
{
    public sealed class AdminResultModerationActionDTO
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
    }
}
