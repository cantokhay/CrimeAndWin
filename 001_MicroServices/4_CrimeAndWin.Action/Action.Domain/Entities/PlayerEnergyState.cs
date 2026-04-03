using Shared.Domain;

namespace Action.Domain.Entities
{
    public class PlayerEnergyState : BaseEntity
    {
        public int  CurrentEnergy       { get; set; }
        public DateTime LastRefillAt    { get; set; }
        public string? ActiveBoostItem  { get; set; }  // null = boost yok
        public DateTime? BoostExpiresAt { get; set; }
    }
}


