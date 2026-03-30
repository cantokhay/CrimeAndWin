namespace Administration.MVC.ViewModels.InventoryVMs.InventoryVMs
{
    public class ResultInventoryVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public string? PlayerDisplay { get; set; }
        public int ItemsCount { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
