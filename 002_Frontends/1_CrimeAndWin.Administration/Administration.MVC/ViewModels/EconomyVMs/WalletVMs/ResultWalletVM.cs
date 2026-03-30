namespace Administration.MVC.ViewModels.EconomyVMs.WalletVMs
{
    public sealed class ResultWalletVM
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }

        public string? PlayerDisplay { get; set; }
    }
}
