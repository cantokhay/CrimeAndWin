using Shared.Domain;

namespace PlayerProfile.Domain.Entities
{
    public sealed class Gang : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Tag { get; set; } = default! ; // e.g. [GNG]
        public string? Description { get; set; }
        public string? LogoKey { get; set; }
        
        // Leadership
        public Guid LeaderId { get; set; } // Player ID of the leader
        
        // Stats
        public decimal TotalRespectScore { get; set; }
        public int MemberCount { get; set; }
        public int MaxMemberLimit { get; set; } = 20; // Default limit
        
        // Levels/Tiers
        public int Level { get; set; } = 1;

        public bool IsActive { get; set; } = true;
    }
}
