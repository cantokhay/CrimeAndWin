namespace Action.Application.DTOs.ActionAttemptDTOs
{
    public class PlayerActionAttemptDTO
    {
        public Guid PlayerId { get; set; }
        public Guid ActionDefinitionId { get; set; }
        public double SuccessRate { get; set; }
    }
}
