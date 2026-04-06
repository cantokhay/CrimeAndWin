using PlayerProfile.Domain.VOs;
using PlayerProfile.Domain.Enums;
using Shared.Domain;

namespace PlayerProfile.Domain.Entities
{
    public sealed class Player : BaseEntity
    {
        public Guid AppUserId { get; set; }        // Identity service'teki AppUser.Id
        public string? DisplayName { get; set; } // Oyun ici gorunen ad
        public string? AvatarKey { get; set; }   // CDN/Sprite anahtari
        public Stats Stats { get; set; } = default!;
        public Energy Energy { get; set; } = default!;
        public Rank Rank { get; set; } = default!;
        
        // Phase 4: Social Feature
        public Guid? GangId { get; set; } // Current Gang the player belongs to
        public GangRole GangRole { get; set; } = GangRole.None;
        
        public decimal HeatIndex { get; set; } // 0 to 100
        public decimal RespectScore { get; set; } // Social Dominance
        public DateTime LastEnergyCalcUtc { get; set; } // offline regen hesabi icin damga
    }
}
