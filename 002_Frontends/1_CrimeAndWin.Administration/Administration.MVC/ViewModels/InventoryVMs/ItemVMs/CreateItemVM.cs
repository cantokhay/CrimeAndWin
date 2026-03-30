using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administration.MVC.ViewModels.InventoryVMs.ItemVMs
{
    public class CreateItemVM
    {
        public Guid InventoryId { get; set; }

        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Power { get; set; }

        public decimal Amount { get; set; }
        public int Currency { get; set; }

        public List<SelectListItem> InventoryOptions { get; set; } = new();
        public List<SelectListItem> CurrencyOptions { get; set; } = new();
    }
}
