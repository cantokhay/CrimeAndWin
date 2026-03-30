using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.GameWorldVMs.SeasonVMs
{
    public class UpdateSeasonVM
    {
        public Guid Id { get; set; }
        public Guid GameWorldId { get; set; }

        public int SeasonNumber { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public bool IsActive { get; set; }

        public string? ReturnUrl { get; set; }

        public List<SelectListItem> GameWorldOptions { get; set; } = new();
    }
}