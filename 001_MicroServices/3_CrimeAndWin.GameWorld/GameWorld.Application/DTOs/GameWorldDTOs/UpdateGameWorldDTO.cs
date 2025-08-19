using GameWorld.Application.DTOs.SeasonDTOs;

namespace GameWorld.Application.DTOs.GameWorldDTOs
{
    public class UpdateGameWorldDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxEnergy { get; set; }
        public int RegenRatePerHour { get; set; }
        public IReadOnlyList<ResultSeasonDTO> Seasons { get; set; }
    }
}
