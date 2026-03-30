namespace Leadership.Application.DTOs.LeaderboardDTOs
{
    public class CreateLeaderboardDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? GameWorldId { get; set; }
        public Guid? SeasonId { get; set; }
        public bool IsSeasonal { get; set; }
    }
}
