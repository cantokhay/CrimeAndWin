using Shared.Domain;

namespace Leadership.Domain.Entities
{
    public class Leaderboard : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }
        public ICollection<LeaderboardEntry> Entries { get; set; } = new List<LeaderboardEntry>();
    }
}
