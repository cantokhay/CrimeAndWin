using MassTransit;

namespace Economy.API.Sagas
{
    public class LaunderingSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;

        public Guid PlayerId { get; set; }
        public decimal InputBlackAmount { get; set; }
        public decimal Efficiency { get; set; }
        public decimal FinalCashAmount { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}
