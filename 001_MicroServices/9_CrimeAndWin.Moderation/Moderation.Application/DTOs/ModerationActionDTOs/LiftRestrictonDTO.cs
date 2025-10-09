namespace Moderation.Application.DTOs.ModerationActionDTOs
{

    public class LiftRestrictionDTO
    {
        public Guid PlayerId { get; set; }
        public Guid ModeratorId { get; set; }
        public string Reason { get; set; } // Kaldırma gerekçesi
    }
}
