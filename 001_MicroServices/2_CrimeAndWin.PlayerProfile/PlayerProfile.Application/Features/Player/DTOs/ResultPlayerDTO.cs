namespace PlayerProfile.Application.Features.Player.DTOs
{
    public sealed class ResultPlayerDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
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
    }

}
