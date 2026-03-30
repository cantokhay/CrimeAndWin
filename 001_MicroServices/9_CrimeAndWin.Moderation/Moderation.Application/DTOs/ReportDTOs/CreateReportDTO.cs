namespace Moderation.Application.DTOs.ReportDTOs
{
    public class CreateReportDTO
    {
        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }
        public string Reason { get; set; }       // VO değer yazısı
        public string Description { get; set; }
    }
}
