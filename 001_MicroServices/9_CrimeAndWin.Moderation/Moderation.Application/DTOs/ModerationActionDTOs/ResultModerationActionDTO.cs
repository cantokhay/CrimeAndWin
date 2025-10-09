namespace Moderation.Application.DTOs.ModerationActionDTOs
{
    public class ResultModerationActionDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string ActionType { get; set; }
        public string Reason { get; set; }
        public Guid ModeratorId { get; set; }
        public DateTime ActionDateUtc { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
