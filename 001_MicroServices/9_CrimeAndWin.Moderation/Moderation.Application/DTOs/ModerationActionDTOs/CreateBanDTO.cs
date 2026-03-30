namespace Moderation.Application.DTOs.ModerationActionDTOs
{
    public class CreateBanDTO
    {
        public Guid PlayerId { get; set; }
        public Guid ModeratorId { get; set; }
        public string Reason { get; set; }
        public DateTime? ExpiryDateUtc { get; set; }
    }
}
