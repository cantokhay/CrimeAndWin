using Action.Domain.Enums;

namespace Action.Application.DTOs.ActionAttemptDTOs
{
    public sealed class ResultPlayerActionAttemptDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public DateTime AttemptedAtUtc { get; set; }
        public double SuccessRate { get; set; }
        public OutcomeType OutcomeType { get; set; }
    }
}
