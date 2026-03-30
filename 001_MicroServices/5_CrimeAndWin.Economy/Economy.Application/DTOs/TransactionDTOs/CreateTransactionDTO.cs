namespace Economy.Application.DTOs.TransactionDTOs
{
    public class CreateTransactionDTO
    {
        public decimal Amount { get; set; }
        public string CurrencyType { get; set; }
        public string ReasonCode { get; set; }
        public string Description { get; set; }
    }
}
