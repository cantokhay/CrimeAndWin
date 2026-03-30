using Administration.MVC.ViewModels.InventoryVMs.ItemVMs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.InventoryVMs.PlayerInventoryItemsVMs
{
    public class PlayerInventoryItemsVM
    {
        public Guid? SelectedPlayerId { get; set; }
        public Guid? SelectedInventoryId { get; set; }

        public List<SelectListItem> PlayerOptions { get; set; } = new();
        public List<SelectListItem> InventoryOptions { get; set; } = new();

        public List<ResultItemVM> Items { get; set; } = new();
    }
}
