using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administration.MVC.ViewModels.NotificationVMs.NotificationVMs
{
    public class CreateNotificationVM
    {
        [Required]
        public Guid PlayerId { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public string Type { get; set; } = "Info";

        public List<SelectListItem> PlayerOptions { get; set; } = new();
        public List<SelectListItem> TypeOptions { get; set; } = new();
    }
}
