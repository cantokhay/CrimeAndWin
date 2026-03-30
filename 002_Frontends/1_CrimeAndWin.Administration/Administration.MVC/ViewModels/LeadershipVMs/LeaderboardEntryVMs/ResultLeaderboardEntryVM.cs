namespace Administration.MVC.ViewModels.LeadershipVMs.LeaderboardEntryVMs
{
    public class ResultLeaderboardEntryVM
    {
        public Guid Id { get; set; }
        public Guid LeaderboardId { get; set; }
        public Guid PlayerId { get; set; }

        public int RankPoints { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }

        public string? LeaderboardDisplay { get; set; }
        public string? PlayerDisplay { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }

}
