using MassTransit;

namespace Action.API.Sagas
{
    public class RaidSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = default!;

        public Guid PlayerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
