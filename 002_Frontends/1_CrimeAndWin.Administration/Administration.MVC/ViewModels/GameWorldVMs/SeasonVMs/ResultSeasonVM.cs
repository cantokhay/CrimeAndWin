namespace Administration.MVC.ViewModels.GameWorldVMs.SeasonVMs
{
    public class ResultSeasonVM
    {
        public Guid Id { get; set; }
        public Guid GameWorldId { get; set; }
        public string GameWorldName { get; set; } = string.Empty;

        public int SeasonNumber { get; set; }
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public bool IsActive { get; set; }
    }
}