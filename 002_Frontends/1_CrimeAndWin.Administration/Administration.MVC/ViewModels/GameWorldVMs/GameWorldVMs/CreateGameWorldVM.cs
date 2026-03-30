namespace Administration.MVC.ViewModels.GameWorldVMs.GameWorldVMs
{
    public class CreateGameWorldVM
    {
        public string Name { get; set; } = null!;
        public int MaxEnergy { get; set; }
        public int RegenRatePerHour { get; set; }
    }
}
