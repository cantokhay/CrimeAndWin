
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administration.MVC.ViewModels.ModerationVMs.ActionVMs
{
    public class CreateModerationActionVM
    {
        public Guid PlayerId { get; set; }
        public string ActionType { get; set; } = "Ban";
        public string Reason { get; set; } = "";

        public DateTime ActionDateUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDateUtc { get; set; }

        public Guid ModeratorId { get; set; }

        public bool IsActive { get; set; } = true;

        public List<SelectListItem> PlayerOptions { get; set; } = new();
        public List<SelectListItem> ActionTypeOptions { get; set; } = new();
    }
}
