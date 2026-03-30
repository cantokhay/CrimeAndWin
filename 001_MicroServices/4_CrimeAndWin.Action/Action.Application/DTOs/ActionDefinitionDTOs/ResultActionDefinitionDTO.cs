using Action.Domain.VOs;

namespace Action.Application.DTOs.ActionDefinitionDTOs
{
    public sealed class ResultActionDefinitionDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int MinPower { get; set; }
        public int EnergyCost { get; set; }
        public int PowerGain { get; set; }
        public bool ItemDrop { get; set; }
        public decimal MoneyGain { get; set; }
        public bool IsActive { get; set; }
    }
}
