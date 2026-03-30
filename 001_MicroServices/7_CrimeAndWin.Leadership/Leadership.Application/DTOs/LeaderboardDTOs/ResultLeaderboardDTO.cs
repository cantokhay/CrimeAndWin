using Leadership.Application.DTOs.LeaderboardEntryDTOs;

namespace Leadership.Application.DTOs.LeaderboardDTOs
{
    public class ResultLeaderboardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }
        public int EntryCount { get; set; }
        public List<ResultLeaderboardEntryDTO> Entries { get; set; } = new();
    }
}
