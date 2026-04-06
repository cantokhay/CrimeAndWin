namespace CrimeAndWin.Contracts.Events.Action
{
    public record RaidStartedEvent
    {
        public Guid CorrelationId { get; init; }
        public Guid PlayerId { get; init; }
        public string Reason { get; init; } = "High Heat Detected";
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}

namespace CrimeAndWin.Contracts.Commands.Economy
{
    public record SeizeBlackMoneyCommand
    {
        public Guid CorrelationId { get; init; }
        public Guid PlayerId { get; init; }
    }
}

namespace CrimeAndWin.Contracts.Commands.PlayerProfile
{
    public record StartPrisonSentenceCommand
    {
        public Guid CorrelationId { get; init; }
        public Guid PlayerId { get; init; }
        public int DurationMinutes { get; init; } = 30;
    }
}
