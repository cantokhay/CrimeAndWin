namespace Leadership.Application.DTOs.LeaderboardEntryDTOs
{
    public class ResultLeaderboardEntryDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public int RankPoints { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
