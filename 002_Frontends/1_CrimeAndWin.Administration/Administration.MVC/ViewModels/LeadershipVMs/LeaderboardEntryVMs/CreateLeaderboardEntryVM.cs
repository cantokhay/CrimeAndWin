using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.LeadershipVMs.LeaderboardEntryVMs
{
    public class CreateLeaderboardEntryVM
    {
        public Guid LeaderboardId { get; set; }
        public Guid PlayerId { get; set; }

        public int RankPoints { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }

        public List<SelectListItem> LeaderboardOptions { get; set; } = new();
        public List<SelectListItem> PlayerOptions { get; set; } = new();
    }
}
