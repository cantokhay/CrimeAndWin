namespace Moderation.Application.DTOs.ModerationActionDTOs
{
    public class CreateRestrictDTO
    {
        public Guid PlayerId { get; set; }
        public Guid ModeratorId { get; set; }
        public string Reason { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
    }
}
