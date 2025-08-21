namespace Leadership.Application.DTOs.LeaderboardEntryDTOs
{
    public class CreateLeaderboardEntryDTO
    {
        public Guid PlayerId { get; set; }
        public int RankPoints { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
