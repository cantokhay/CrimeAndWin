using PlayerProfile.Domain.VOs;
using Shared.Domain;

namespace PlayerProfile.Domain.Entities
{
    public sealed class Player : BaseEntity
    {
        public Guid AppUserId { get; set; }        // Identity service'teki AppUser.Id
        public string? DisplayName { get; set; } // Oyun içi görünen ad
        public string? AvatarKey { get; set; }   // CDN/Sprite anahtarı
        public Stats Stats { get; set; } = default!;
        public Energy Energy { get; set; } = default!;
        public Rank Rank { get; set; } = default!;
        public DateTime LastEnergyCalcUtc { get; set; } // offline regen hesabı için damga
    }
}
