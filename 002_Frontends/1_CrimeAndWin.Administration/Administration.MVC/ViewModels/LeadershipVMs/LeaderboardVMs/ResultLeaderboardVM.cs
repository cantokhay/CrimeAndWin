namespace Administration.MVC.ViewModels.LeadershipVMs.LeaderboardVMs
{
    public class ResultLeaderboardVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }

        public int EntriesCount { get; set; } // UI için kolaylık
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
