using Administration.MVC.ViewModels.GameWorldVMs.SeasonVMs;

namespace Administration.MVC.ViewModels.GameWorldVMs.GameWorldVMs
{
    public class ResultGameWorldVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int MaxEnergy { get; set; }
        public int RegenRatePerHour { get; set; }

        public IReadOnlyList<ResultSeasonVM> Seasons { get; set; } = new List<ResultSeasonVM>();

        public int SeasonsCount => Seasons?.Count ?? 0;
    }
}
