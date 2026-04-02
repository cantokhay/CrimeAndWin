namespace Moderation.Application.DTOs.ReportDTOs
{
    public class CreateReportDTO
    {
        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }
        public string Reason { get; set; }       // VO deðer yazýsý
        public string Description { get; set; }
    }
}

