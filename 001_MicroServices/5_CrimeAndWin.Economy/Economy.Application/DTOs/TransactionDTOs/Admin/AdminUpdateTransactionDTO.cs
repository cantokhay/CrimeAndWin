namespace Economy.Application.DTOs.TransactionDTOs.Admin
{
    public sealed class AdminUpdateTransactionDTO
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; } = null!;
        public string ReasonCode { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
