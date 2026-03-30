using Administration.MVC.ViewModels.GameWorldVMs.SeasonVMs;

namespace Administration.MVC.ViewModels.GameWorldVMs.GameWorldVMs
{
    public class UpdateGameWorldVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int MaxEnergy { get; set; }
        public int RegenRatePerHour { get; set; }

        public List<ResultSeasonVM> Seasons { get; set; } = new();
    }
}