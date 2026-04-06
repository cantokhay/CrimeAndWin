using Shared.Domain;

namespace Economy.Domain.Entities
{
    public sealed class GangWallet : BaseEntity
    {
        public Guid GangId { get; set; } // Reference to the Gang in PlayerProfile
        
        // Balances
        public decimal BlackBalance { get; set; } // Dirty money of the gang
        public decimal CashBalance { get; set; }  // Cleaned money of the gang
        
        // Limits
        public decimal MaxCapacity { get; set; } = 1000000; // Default limit
        
        // Metadata
        public string? Note { get; set; }
        public DateTime LastTransactionAt { get; set; }
    }
}
