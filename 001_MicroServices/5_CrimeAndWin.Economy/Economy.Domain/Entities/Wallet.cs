using Shared.Domain;

namespace Economy.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; } // Legacy support
        public decimal BlackBalance { get; set; } // Crime/Action rewards
        public decimal CashBalance { get; set; }  // Passive/Trade rewards

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}


