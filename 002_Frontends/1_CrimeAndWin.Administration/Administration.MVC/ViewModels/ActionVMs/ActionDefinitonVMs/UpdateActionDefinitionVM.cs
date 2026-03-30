namespace Administration.MVC.ViewModels.ActionVMs.ActionDefinitonVMs
{
    public class UpdateActionDefinitionVM
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MinPower { get; set; }
        public int EnergyCost { get; set; }
        public int PowerGain { get; set; }
        public bool ItemDrop { get; set; }
        public decimal MoneyGain { get; set; }
        public bool IsActive { get; set; }
    }
}
