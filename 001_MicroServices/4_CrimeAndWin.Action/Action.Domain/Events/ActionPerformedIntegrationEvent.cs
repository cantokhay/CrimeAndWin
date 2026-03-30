namespace Action.Domain.Events
{
    public sealed class ActionPerformedIntegrationEvent
    {
        public Guid PlayerId { get; init; }
        public Guid ActionDefinitionId { get; init; }
        public int PowerGain { get; init; }
        public bool ItemDrop { get; init; }
        public decimal MoneyGain { get; init; }
        public DateTime OccurredAtUtc { get; init; }
    }
}
