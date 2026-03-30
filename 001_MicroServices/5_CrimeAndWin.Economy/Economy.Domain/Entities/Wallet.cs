using Shared.Domain;

namespace Economy.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public Guid PlayerId { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
