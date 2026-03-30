using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.InventoryVMs.InventoryVMs
{
    public class CreateInventoryVM
    {
        public Guid PlayerId { get; set; }

        public List<SelectListItem> PlayerOptions { get; set; } = new();
    }
}
