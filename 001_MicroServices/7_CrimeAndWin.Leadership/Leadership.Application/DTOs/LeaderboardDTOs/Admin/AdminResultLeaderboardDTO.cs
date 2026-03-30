using Leadership.Application.DTOs.LeaderboardEntryDTOs.Admin;

namespace Leadership.Application.DTOs.LeaderboardDTOs.Admin
{
    public sealed class AdminResultLeaderboardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }

        public List<AdminResultLeaderboardEntryDTO> Entries { get; set; } = new();

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }
}
