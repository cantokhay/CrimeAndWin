namespace Action.Application.DTOs.ActionDefinitionDTOs
{
    public class CreateActionDefinitionDTO
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int MinPower { get; set; }
        public int EnergyCost { get; set; }
        public int PowerGain { get; set; }
        public bool ItemDrop { get; set; }
        public decimal MoneyGain { get; set; }
        public bool IsActive { get; set; }
    }
}
