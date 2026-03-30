namespace Leadership.Application.DTOs.LeaderboardDTOs.Admin
{
    public sealed class AdminUpdateLeaderboardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }
    }
}
