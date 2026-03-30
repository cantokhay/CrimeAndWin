using Economy.Domain.VOs;
using Shared.Domain;

namespace Economy.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Money Money { get; set; }
        public TransactionReason Reason { get; set; }

        public Wallet Wallet { get; set; }
    }
}
