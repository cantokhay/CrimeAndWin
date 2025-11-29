using Action.Domain.VOs;

namespace Action.Application.DTOs.ActionDefinitionDTOs
{
    public class ActionDefinitionDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ActionRequirements Requirements { get; set; }
        public ActionRewards Rewards { get; set; }
        public bool IsActive { get; set; }
    }
}
