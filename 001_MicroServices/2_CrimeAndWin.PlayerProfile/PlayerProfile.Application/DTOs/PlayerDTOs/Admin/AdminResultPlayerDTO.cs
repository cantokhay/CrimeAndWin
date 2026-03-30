namespace PlayerProfile.Application.DTOs.PlayerDTOs.Admin
{
    public sealed class AdminResultPlayerDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public string? DisplayName { get; set; }
        public string? AvatarKey { get; set; }

        public int Power { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }

        public int EnergyCurrent { get; set; }
        public int EnergyMax { get; set; }
        public int EnergyRegenPerMinute { get; set; }

        public int RankPoints { get; set; }
        public int? RankPosition { get; set; }

        public DateTime LastEnergyCalcUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
