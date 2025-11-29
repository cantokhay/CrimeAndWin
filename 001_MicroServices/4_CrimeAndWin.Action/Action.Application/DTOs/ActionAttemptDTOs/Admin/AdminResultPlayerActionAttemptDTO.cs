namespace Action.Application.DTOs.ActionAttemptDTOs.Admin
{
    public sealed class AdminResultPlayerActionAttemptDTO
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; }  // 0.0 - 1.0
        public string OutcomeType { get; set; } = null!; // Success / Fail
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
    }

}
