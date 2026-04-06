using Shared.Domain;

namespace GameWorld.Domain.Entities
{
    public sealed class District : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        
        // Ownership
        public Guid? OwnerId { get; set; } // Current Player who owns the district
        public string? OwnerName { get; set; }
        
        // Stats
        public decimal TotalRespectPoints { get; set; } // Total respect generated in this district
        public decimal TaxRate { get; set; } = 0.05m; // 5% default tax for the owner
        
        // Buffs/Modifiers (Optional)
        public decimal MoneyBonusMultiplier { get; set; } = 1.0m;
        public decimal HeatDecayMultiplier { get; set; } = 1.0m;
        
        public bool IsActive { get; set; } = true;
    }
}
