namespace GameWorld.Application.DTOs.SeasonDTOs
{
    public class ResultSeasonDTO
    {
        public Guid Id { get; set; }
        public int SeasonNumber { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
