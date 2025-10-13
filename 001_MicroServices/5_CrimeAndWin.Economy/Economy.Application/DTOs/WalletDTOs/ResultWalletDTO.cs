using Economy.Application.DTOs.TransactionDTOs;

namespace Economy.Application.DTOs.WalletDTOs
{
    public sealed class ResultWalletDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }
        public List<ResultTransactionDTO> Transactions { get; set; } = new();
    }
}