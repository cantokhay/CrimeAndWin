using Action.Domain.VOs;
using Action.Domain.Enums;
using Shared.Domain;

namespace Action.Domain.Entities
{
    public class ActionDefinition : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Description { get; set; } = default!;
        
        // New Mechanics
        public ActionType Type { get; set; } = ActionType.Crime;
        public decimal BaseSuccessRate { get; set; } // 0 to 100
        public decimal HeatImpact { get; set; } // Heat increase on crime or decrease on bribe
        public decimal RespectImpact { get; set; } // Respect gain or loss
        
        public ActionRequirements Requirements { get; set; } = default!;
        public ActionRewards Rewards { get; set; } = default!;
        public bool IsActive { get; set; } = true;
    }
}
