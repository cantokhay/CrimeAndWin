using Action.Domain.VOs;
using Shared.Domain;

namespace Action.Domain.Entities
{
    public class ActionDefinition : BaseEntity
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ActionRequirements Requirements { get; set; }
        public ActionRewards Rewards { get; set; }
        public bool IsActive { get; set; }
    }
}
