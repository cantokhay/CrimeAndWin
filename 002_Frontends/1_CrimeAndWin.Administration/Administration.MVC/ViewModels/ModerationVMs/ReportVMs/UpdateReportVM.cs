using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administration.MVC.ViewModels.ModerationVMs.ReportVMs
{
    public class UpdateReportVM
    {
        public Guid Id { get; set; }
        public Guid ReporterId { get; set; }
        public Guid ReportedPlayerId { get; set; }
        public string Reason { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsResolved { get; set; }
        public DateTime? ResolvedAtUtc { get; set; }
        public Guid? ResolvedByModeratorId { get; set; }

        public List<SelectListItem> PlayerOptions { get; set; } = new();
    }
}
