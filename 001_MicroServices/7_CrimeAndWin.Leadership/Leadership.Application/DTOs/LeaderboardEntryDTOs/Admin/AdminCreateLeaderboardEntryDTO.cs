namespace Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin
{
    public sealed class AdminCreateLeaderboardEntryDTO
    {
        public Guid LeaderboardId { get; set; }
        public Guid PlayerId { get; set; }

        public int RankPoints { get; set; }
        public int Position { get; set; }

        public bool IsActive { get; set; }
    }
}
