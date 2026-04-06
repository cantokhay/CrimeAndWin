using MassTransit;

namespace Action.API.Sagas
{
    public class CrimeActionState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;

        // Saga Context Data
        public Guid PlayerId { get; set; }
        public Guid ActionId { get; set; }
        public string ActionCode { get; set; } = default!;
        
        public decimal MoneyReward { get; set; }
        public decimal HeatImpact { get; set; }
        public bool IsSuccess { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
