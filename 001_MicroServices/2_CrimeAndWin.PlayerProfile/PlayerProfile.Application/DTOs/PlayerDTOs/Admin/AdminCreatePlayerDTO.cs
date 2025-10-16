namespace PlayerProfile.Application.DTOs.PlayerDTOs.Admin
{
    public sealed class AdminCreatePlayerDTO
    {
        public Guid AppUserId { get; set; }
        public string? DisplayName { get; set; }
        public string? AvatarKey { get; set; }

        // Stats
        public int Power { get; set; }
        public int Defense { get; set; }
        public int Agility { get; set; }
        public int Luck { get; set; }

        // Energy
        public int EnergyCurrent { get; set; }
        public int EnergyMax { get; set; }
        public int EnergyRegenPerMinute { get; set; }

        // Rank
        public int RankPoints { get; set; }
        public int? RankPosition { get; set; }

        public DateTime LastEnergyCalcUtc { get; set; }
    }
}
