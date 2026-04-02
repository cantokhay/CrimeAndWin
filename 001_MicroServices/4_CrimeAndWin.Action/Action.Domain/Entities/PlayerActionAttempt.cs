using Action.Domain.VOs;
using Shared.Domain;

namespace Action.Domain.Entities
{
    public class PlayerActionAttempt : BaseEntity
    {
        public Guid CorrelationId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public DateTime AttemptedAtUtc { get; set; }
        public PlayerActionResult PlayerActionResults { get; set; }
        
        public DateTime CooldownEndsAt  { get; set; }
        public bool     IsSuccess       { get; set; }
        public double   SuccessRate     { get; set; }
    }
}

