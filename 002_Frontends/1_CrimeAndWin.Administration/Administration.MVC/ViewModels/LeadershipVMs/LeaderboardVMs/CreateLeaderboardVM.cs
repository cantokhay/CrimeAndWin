using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.LeadershipVMs.LeaderboardVMs
{
    public class CreateLeaderboardVM
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }

        public List<SelectListItem> GameWorldOptions { get; set; } = new();
        public List<SelectListItem> SeasonOptions { get; set; } = new();
    }
}
