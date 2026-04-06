using Economy.Domain.Enums;
using Economy.Domain.VOs;
using Shared.Domain;

namespace Economy.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid WalletId { get; set; }
        public Money Money { get; set; }
        public TransactionReason Reason { get; set; }
        public WalletBalanceType BalanceType { get; set; } // Which balance was affected?

        public Wallet Wallet { get; set; }
    }
}


