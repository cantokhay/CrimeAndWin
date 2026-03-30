namespace Administration.MVC.ViewModels.InventoryVMs.ItemVMs
{
    public class ResultItemVM
    {
        public Guid Id { get; set; }
        public Guid InventoryId { get; set; }

        public string Name { get; set; } = null!;
        public int Quantity { get; set; }

        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Power { get; set; }

        public decimal Amount { get; set; }
        public int Currency { get; set; } 

        public string? InventoryDisplay { get; set; }
        public string? CurrencyText { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
