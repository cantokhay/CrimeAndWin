namespace Administration.MVC.ViewModels.EconomyVMs.TransactionVMs
{
    public sealed class ResultTransactionVM
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; } = null!;
        public string ReasonCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }

        public string? WalletDisplay { get; set; }
    }
}
