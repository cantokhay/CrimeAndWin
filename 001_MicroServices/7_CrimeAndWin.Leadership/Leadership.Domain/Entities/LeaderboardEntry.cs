using Leadership.Domain.VOs;
using Shared.Domain;

namespace Leadership.Domain.Entities
{
    public class LeaderboardEntry : BaseEntity
    {
        public Guid LeaderboardId { get; set; }
        public Guid PlayerId { get; set; }
        public Rank Rank { get; set; }
        public bool IsActive { get; set; }

        public Leaderboard Leaderboard { get; set; }
    }
}
