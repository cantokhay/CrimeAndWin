namespace GameWorld.Application.DTOs.SeasonDTOs
{
    public class CreateSeasonDTO
    {
        public int SeasonNumber { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
