namespace Economy.Application.DTOs.TransactionDTOs
{
    public class ResultTransactionDTO
    {
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Deposit/Withdraw vb.
        public DateTime CreatedAtUtc { get; set; }

        public string? ReasonCode { get; set; }
        public string? Description { get; set; }
    }
}
