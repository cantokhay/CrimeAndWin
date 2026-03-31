namespace CrimeAndWin.Contracts.Events.PlayerProfile;

public record PlayerStatsUpdatedEvent
{
    public Guid CorrelationId { get; init; }
    public Guid PlayerId      { get; init; }
    public bool IsSuccess     { get; init; }
    public string? FailReason { get; init; }
}
