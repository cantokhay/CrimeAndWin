namespace Action.Application.DTOs.ActionAttemptDTOs.Admin
{
    public sealed class AdminCreatePlayerActionAttemptDTO
    {
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; }  // 0.0 - 1.0
    }
}
