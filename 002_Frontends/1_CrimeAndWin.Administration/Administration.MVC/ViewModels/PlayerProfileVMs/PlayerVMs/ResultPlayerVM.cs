namespace Administration.MVC.ViewModels.PlayerProfileVMs.PlayerVMs
{
    public class ResultPlayerVM
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }

        public string? AppUserName { get; set; } 
        public string? AppUserEmail { get; set; }

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

        // Faz 4 Metrikleri
        public decimal HeatIndex { get; set; } = 0;
        public decimal RespectScore { get; set; } = 0;

        // Rank
        public int RankPoints { get; set; }
        public int? RankPosition { get; set; }

        public DateTime LastEnergyCalcUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
